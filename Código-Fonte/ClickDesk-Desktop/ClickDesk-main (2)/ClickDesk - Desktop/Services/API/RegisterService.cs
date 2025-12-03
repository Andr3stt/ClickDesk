using System.Threading.Tasks;
using ClickDesk.Models;

namespace ClickDesk.Services.API
{
    /// <summary>
    /// Serviço para operações de registro e verificação de email.
    /// Gerencia registro de novo usuário, verificação de email e definição de senha.
    /// </summary>
    public static class RegisterService
    {
        /// <summary>
        /// Realiza o registro de um novo usuário.
        /// </summary>
        /// <param name="nome">Primeiro nome do usuário</param>
        /// <param name="sobrenome">Sobrenome do usuário</param>
        /// <param name="email">Email do usuário</param>
        /// <returns>Resposta com dados do usuário registrado</returns>
        public static async Task<ApiResponse<User>> RegisterAsync(string nome, string sobrenome, string email)
        {
            var request = new RegisterRequest
            {
                Nome = nome,
                Sobrenome = sobrenome,
                Email = email
            };

            return await ApiService.PostAsync<RegisterRequest, ApiResponse<User>>(
                ApiConfig.RegisterEndpoint,
                request
            );
        }

        /// <summary>
        /// Verifica o email do usuário usando o token enviado.
        /// </summary>
        /// <param name="verificationToken">Token de verificação enviado por email</param>
        /// <returns>Resposta com informações para definir a senha</returns>
        public static async Task<ApiResponse<VerifyEmailResponse>> VerifyEmailAsync(string verificationToken)
        {
            string url = $"{ApiConfig.BaseUrl}/auth/verify-email?token={verificationToken}";
            return await ApiService.PostAsync<ApiResponse<VerifyEmailResponse>>(url);
        }

        /// <summary>
        /// Define a senha do usuário após verificação de email.
        /// </summary>
        /// <param name="token">Token de definição de senha</param>
        /// <param name="password">Nova senha</param>
        /// <returns>Resposta com mensagem de sucesso</returns>
        public static async Task<ApiResponse<string>> SetPasswordAsync(string token, string password)
        {
            var request = new SetPasswordRequest
            {
                Token = token,
                Password = password
            };

            string url = $"{ApiConfig.BaseUrl}/auth/set-password";

            return await ApiService.PostAsync<SetPasswordRequest, ApiResponse<string>>(url, request);
        }
    }
}
