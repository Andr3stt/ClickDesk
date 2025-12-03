using System.Collections.Generic;
using System.Threading.Tasks;
using ClickDesk.Models;

namespace ClickDesk.Services.API
{
    /// <summary>
    /// Serviço para operações com usuários.
    /// Gerencia perfil, listagem e criação de usuários.
    /// </summary>
    public static class UserService
    {
        /// <summary>
        /// Obtém o perfil do usuário logado.
        /// </summary>
        /// <returns>Dados do usuário</returns>
        public static async Task<ApiResponse<User>> GetMyProfileAsync()
        {
            return await ApiService.GetAsync<ApiResponse<User>>(ApiConfig.ProfileEndpoint);
        }

        /// <summary>
        /// Atualiza o perfil do usuário logado.
        /// </summary>
        /// <param name="user">Dados atualizados</param>
        /// <returns>Perfil atualizado</returns>
        public static async Task<ApiResponse<User>> UpdateProfileAsync(User user)
        {
            return await ApiService.PutAsync<User, ApiResponse<User>>(ApiConfig.ProfileEndpoint, user);
        }

        /// <summary>
        /// Altera a senha do usuário logado.
        /// </summary>
        /// <param name="currentPassword">Senha atual</param>
        /// <param name="newPassword">Nova senha</param>
        /// <returns>Resposta com mensagem de sucesso</returns>
        public static async Task<ApiResponse<string>> ChangePasswordAsync(string currentPassword, string newPassword)
        {
            var request = new
            {
                currentPassword = currentPassword,
                newPassword = newPassword
            };

            return await ApiService.PutAsync<object, ApiResponse<string>>(
                ApiConfig.ChangePasswordEndpoint,
                request
            );
        }

        /// <summary>
        /// Lista todos os usuários (admin).
        /// </summary>
        /// <returns>Lista de usuários</returns>
        public static async Task<List<User>> ListAllAsync()
        {
            return await ApiService.GetAsync<List<User>>(ApiConfig.UsersEndpoint);
        }

        /// <summary>
        /// Obtém um usuário específico (admin).
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <returns>Dados do usuário</returns>
        public static async Task<User> GetByIdAsync(int id)
        {
            return await ApiService.GetAsync<User>(ApiConfig.UserEndpoint(id));
        }

        /// <summary>
        /// Cria um novo usuário (admin).
        /// </summary>
        /// <param name="request">Dados do registro</param>
        /// <returns>Usuário criado</returns>
        public static async Task<User> CreateAsync(RegisterRequest request)
        {
            return await ApiService.PostAsync<RegisterRequest, User>(ApiConfig.UsersEndpoint, request);
        }

        /// <summary>
        /// Atualiza um usuário (admin).
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <param name="user">Dados atualizados</param>
        /// <returns>Usuário atualizado</returns>
        public static async Task<User> UpdateAsync(int id, User user)
        {
            return await ApiService.PutAsync<User, User>(ApiConfig.UserEndpoint(id), user);
        }

        /// <summary>
        /// Exclui um usuário (admin).
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <returns>Resposta da exclusão</returns>
        public static async Task<ApiResponse<object>> DeleteAsync(int id)
        {
            return await ApiService.DeleteAsync<ApiResponse<object>>(ApiConfig.UserEndpoint(id));
        }
    }
}
