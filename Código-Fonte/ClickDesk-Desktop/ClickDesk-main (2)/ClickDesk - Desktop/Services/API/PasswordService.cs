using System.Threading.Tasks;
using ClickDesk.Models;

namespace ClickDesk.Services.API
{
    /// <summary>
    /// Serviço para operações de recuperação e redefinição de senha.
    /// Gerencia recuperação de senha perdida, validação de token e redefinição.
    /// </summary>
    public static class PasswordService
    {
        /// <summary>
        /// Envia um email com link de recuperação de senha.
        /// </summary>
        /// <param name="email">Email do usuário</param>
        /// <returns>Resposta com mensagem de sucesso</returns>
        public static async Task<ApiResponse<string>> SendRecoveryEmailAsync(string email)
        {
            var request = new ForgotPasswordRequest
            {
                Email = email
            };

            string url = $"{ApiConfig.BaseUrl}/auth/forgot-password";

            return await ApiService.PostAsync<ForgotPasswordRequest, ApiResponse<string>>(url, request);
        }

        /// <summary>
        /// Valida um token de recuperação de senha.
        /// </summary>
        /// <param name="token">Token de recuperação enviado por email</param>
        /// <returns>Resposta com informações do token</returns>
        public static async Task<ApiResponse<object>> ValidateRecoveryTokenAsync(string token)
        {
            string url = $"{ApiConfig.BaseUrl}/auth/validate-recovery?token={token}";
            return await ApiService.PostAsync<ApiResponse<object>>(url);
        }

        /// <summary>
        /// Redefine a senha usando o token de recuperação.
        /// </summary>
        /// <param name="token">Token de recuperação</param>
        /// <param name="newPassword">Nova senha</param>
        /// <returns>Resposta com mensagem de sucesso</returns>
        public static async Task<ApiResponse<string>> ResetPasswordAsync(string token, string newPassword)
        {
            var request = new SetPasswordRequest
            {
                Token = token,
                Password = newPassword
            };

            string url = $"{ApiConfig.BaseUrl}/auth/reset-password";

            return await ApiService.PostAsync<SetPasswordRequest, ApiResponse<string>>(url, request);
        }
    }
}
