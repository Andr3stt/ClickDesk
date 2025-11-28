using System.Threading.Tasks;
using ClickDesk.Models;
using ClickDesk.Utils;

namespace ClickDesk.Services.API
{
    /// <summary>
    /// Serviço de autenticação.
    /// Gerencia login, logout, registro e renovação de token.
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
            
            // Chama a API de login
            var response = await ApiService.PostAsync<LoginRequest, LoginResponse>(
                ApiConfig.LoginEndpoint, 
                request
            );

            // Se login bem-sucedido, configura o token e salva a sessão
            if (response != null && !string.IsNullOrEmpty(response.Token))
            {
                ApiService.SetAuthToken(response.Token);
                SessionManager.SaveSession(response.Token, response.User);
            }

            return response;
        }

        /// <summary>
        /// Realiza o registro de um novo usuário.
        /// </summary>
        /// <param name="request">Dados do registro</param>
        /// <returns>Resposta com dados do usuário criado</returns>
        public static async Task<ApiResponse<User>> RegisterAsync(RegisterRequest request)
        {
            return await ApiService.PostAsync<RegisterRequest, ApiResponse<User>>(
                ApiConfig.RegisterEndpoint,
                request
            );
        }

        /// <summary>
        /// Realiza o logout do usuário.
        /// </summary>
        public static async Task LogoutAsync()
        {
            try
            {
                // Tenta notificar o servidor (opcional, pode falhar)
                await ApiService.PostAsync<object>(ApiConfig.LogoutEndpoint);
            }
            catch
            {
                // Ignora erros - o logout local é mais importante
            }
            finally
            {
                // Sempre limpa a sessão local
                ApiService.ClearAuthToken();
                SessionManager.ClearSession();
            }
        }

        /// <summary>
        /// Tenta renovar o token de autenticação.
        /// </summary>
        public static async Task<bool> RefreshTokenAsync()
        {
            try
            {
                var response = await ApiService.PostAsync<LoginResponse>(ApiConfig.RefreshTokenEndpoint);
                
                if (response != null && !string.IsNullOrEmpty(response.Token))
                {
                    ApiService.SetAuthToken(response.Token);
                    SessionManager.SaveSession(response.Token, response.User ?? SessionManager.CurrentUser);
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
        public static bool IsAuthenticated()
        {
            return SessionManager.IsLoggedIn && !SessionManager.IsTokenExpired();
        }

        /// <summary>
        /// Restaura a sessão do usuário se houver token válido salvo.
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
