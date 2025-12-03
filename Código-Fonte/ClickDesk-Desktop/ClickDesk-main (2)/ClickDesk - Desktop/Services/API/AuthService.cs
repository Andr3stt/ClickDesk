using System.Threading.Tasks;
using ClickDesk.Models;
using ClickDesk.Utils;

namespace ClickDesk.Services.API
{
    /// <summary>
    /// Serviço de autenticação.
    /// Gerencia login, logout, e validação de sessão.
    /// Operações de registro e recuperação de senha estão em RegisterService e PasswordService.
    /// </summary>
    public static class AuthService
    {
        /// <summary>
        /// Realiza o login do usuário.
        /// </summary>
        /// <param name="username">Nome de usuário ou email</param>
        /// <param name="password">Senha</param>
        /// <returns>Resposta de login com token e dados do usuário</returns>
        public static async Task<LoginResponse> LoginAsync(string username, string password)
        {
            var request = new LoginRequest(username, password);
            var response = await ApiService.PostAsync<LoginRequest, LoginResponse>(
                ApiConfig.LoginEndpoint,
                request
            );

            if (response != null && !string.IsNullOrEmpty(response.Token))
            {
                ApiService.SetAuthToken(response.Token);
                SessionManager.SaveSession(response.Token, response.Username);
            }

            return response;
        }

        /// <summary>
        /// Realiza o logout do usuário.
        /// </summary>
        public static void Logout()
        {
            ApiService.ClearAuthToken();
            SessionManager.ClearSession();
        }

        /// <summary>
        /// Tenta renovar o token de autenticação.
        /// </summary>
        /// <returns>Novo token se bem-sucedido, null caso contrário</returns>
        public static async Task<bool> RefreshTokenAsync()
        {
            try
            {
                var response = await ApiService.PostAsync<LoginResponse>(ApiConfig.RefreshTokenEndpoint);

                if (response != null && !string.IsNullOrEmpty(response.Token))
                {
                    ApiService.SetAuthToken(response.Token);
                    SessionManager.SaveSession(response.Token, response.Username);
                    return true;
                }
            }
            catch
            {
                // Token expirado ou inválido
            }

            return false;
        }

        /// <summary>
        /// Verifica se o usuário está autenticado.
        /// </summary>
        /// <returns>True se autenticado, false caso contrário</returns>
        public static bool IsAuthenticated()
        {
            return SessionManager.IsLoggedIn && !SessionManager.IsTokenExpired();
        }

        /// <summary>
        /// Restaura a sessão do usuário se houver token válido salvo.
        /// Deve ser chamado na inicialização da aplicação.
        /// </summary>
        public static void RestoreSession()
        {
            if (SessionManager.IsLoggedIn && !string.IsNullOrEmpty(SessionManager.Token))
            {
                ApiService.SetAuthToken(SessionManager.Token);
            }
        }
    }
}
