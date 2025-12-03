using System.Collections.Generic;
using System.Threading.Tasks;
using ClickDesk.Models;

namespace ClickDesk.Services.API
{
    /// <summary>
    /// Serviço para operações com chamados/tickets.
    /// Gerencia criação, listagem, atualização e feedback de chamados.
    /// </summary>
    public static class ChamadoService
    {
        /// <summary>
        /// Lista todos os chamados (para admin/tech).
        /// </summary>
        /// <returns>Lista de chamados</returns>
        public static async Task<List<Chamado>> ListAllAsync()
        {
            return await ApiService.GetAsync<List<Chamado>>(ApiConfig.ChamadosEndpoint);
        }

        /// <summary>
        /// Lista os chamados do usuário logado.
        /// </summary>
        /// <returns>Lista de chamados do usuário</returns>
        public static async Task<List<Chamado>> ListMyAsync()
        {
            return await ApiService.GetAsync<List<Chamado>>(ApiConfig.MeusChamadosEndpoint);
        }

        /// <summary>
        /// Obtém um chamado específico pelo ID.
        /// </summary>
        /// <param name="id">ID do chamado</param>
        /// <returns>Dados do chamado</returns>
        public static async Task<Chamado> GetByIdAsync(int id)
        {
            return await ApiService.GetAsync<Chamado>(ApiConfig.ChamadoEndpoint(id));
        }

        /// <summary>
        /// Cria um novo chamado.
        /// A IA pode resolver automaticamente se for baixa/média severidade.
        /// </summary>
        /// <param name="request">Dados do chamado</param>
        /// <returns>Chamado criado</returns>
        public static async Task<Chamado> CreateAsync(ChamadoRequest request)
        {
            return await ApiService.PostAsync<ChamadoRequest, Chamado>(
                ApiConfig.ChamadosEndpoint,
                request
            );
        }

        /// <summary>
        /// Atualiza um chamado existente.
        /// </summary>
        /// <param name="id">ID do chamado</param>
        /// <param name="request">Dados atualizados</param>
        /// <returns>Chamado atualizado</returns>
        public static async Task<Chamado> UpdateAsync(int id, ChamadoRequest request)
        {
            return await ApiService.PutAsync<ChamadoRequest, Chamado>(
                ApiConfig.ChamadoEndpoint(id),
                request
            );
        }

        /// <summary>
        /// Atualiza o status de um chamado.
        /// </summary>
        /// <param name="chamadoId">ID do chamado</param>
        /// <param name="newStatus">Novo status</param>
        /// <returns>Chamado atualizado</returns>
        public static async Task<Chamado> UpdateStatusAsync(int chamadoId, string newStatus)
        {
            var request = new { status = newStatus };

            return await ApiService.PutAsync<object, Chamado>(
                ApiConfig.ChamadoStatusEndpoint(chamadoId),
                request
            );
        }

        /// <summary>
        /// Envia feedback sobre a solução da IA/técnico.
        /// </summary>
        /// <param name="chamadoId">ID do chamado</param>
        /// <param name="feedback">Dados do feedback</param>
        /// <returns>Chamado atualizado</returns>
        public static async Task<Chamado> SendFeedbackAsync(int chamadoId, FeedbackDTO feedback)
        {
            return await ApiService.PostAsync<FeedbackDTO, Chamado>(
                ApiConfig.ChamadoFeedbackEndpoint(chamadoId),
                feedback
            );
        }

        /// <summary>
        /// Adiciona um comentário ao chamado.
        /// </summary>
        /// <param name="chamadoId">ID do chamado</param>
        /// <param name="comentario">Texto do comentário</param>
        /// <returns>Resposta da operação</returns>
        public static async Task<ApiResponse<object>> AddCommentAsync(int chamadoId, string comentario)
        {
            var request = new { comentario = comentario };

            return await ApiService.PostAsync<object, ApiResponse<object>>(
                ApiConfig.ChamadoComentariosEndpoint(chamadoId),
                request
            );
        }

        /// <summary>
        /// Lista os comentários de um chamado.
        /// </summary>
        /// <param name="chamadoId">ID do chamado</param>
        /// <returns>Lista de comentários</returns>
        public static async Task<List<object>> ListCommentsAsync(int chamadoId)
        {
            return await ApiService.GetAsync<List<object>>(
                ApiConfig.ChamadoComentariosEndpoint(chamadoId)
            );
        }

        /// <summary>
        /// Exclui um chamado (apenas admin).
        /// </summary>
        /// <param name="id">ID do chamado</param>
        /// <returns>Resposta da exclusão</returns>
        public static async Task<ApiResponse<object>> DeleteAsync(int id)
        {
            return await ApiService.DeleteAsync<ApiResponse<object>>(ApiConfig.ChamadoEndpoint(id));
        }
    }
}
