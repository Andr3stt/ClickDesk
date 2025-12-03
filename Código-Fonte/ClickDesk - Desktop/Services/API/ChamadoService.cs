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
        public static async Task<List<Chamado>> ListarTodosAsync()
        {
            return await ApiService.GetAsync<List<Chamado>>(ApiConfig.ChamadosEndpoint);
        }

        /// <summary>
        /// Lista os chamados do usuário logado.
        /// </summary>
        /// <returns>Lista de chamados do usuário</returns>
        public static async Task<List<Chamado>> ListarMeusAsync()
        {
            return await ApiService.GetAsync<List<Chamado>>(ApiConfig.MeusChamadosEndpoint);
        }

        /// <summary>
        /// Obtém um chamado específico pelo ID.
        /// </summary>
        /// <param name="id">ID do chamado</param>
        /// <returns>Dados do chamado</returns>
        public static async Task<Chamado> ObterAsync(int id)
        {
            return await ApiService.GetAsync<Chamado>(ApiConfig.ChamadoEndpoint(id));
        }

        /// <summary>
        /// Cria um novo chamado.
        /// A IA pode resolver automaticamente se for baixa/média severidade.
        /// </summary>
        /// <param name="request">Dados do chamado</param>
        /// <returns>Resposta com chamado criado e informações da IA</returns>
        public static async Task<ChamadoResponse> CriarAsync(ChamadoRequest request)
        {
            return await ApiService.PostAsync<ChamadoRequest, ChamadoResponse>(
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
        public static async Task<Chamado> AtualizarAsync(int id, ChamadoRequest request)
        {
            return await ApiService.PutAsync<ChamadoRequest, Chamado>(
                ApiConfig.ChamadoEndpoint(id),
                request
            );
        }

        /// <summary>
        /// Envia feedback sobre a solução da IA.
        /// </summary>
        /// <param name="chamadoId">ID do chamado</param>
        /// <param name="feedback">Dados do feedback</param>
        /// <returns>Chamado atualizado</returns>
        public static async Task<Chamado> EnviarFeedbackAsync(int chamadoId, FeedbackRequest feedback)
        {
            return await ApiService.PostAsync<FeedbackRequest, Chamado>(
                ApiConfig.ChamadoFeedbackEndpoint(chamadoId),
                feedback
            );
        }

        /// <summary>
        /// Atualiza o status de um chamado.
        /// </summary>
        /// <param name="chamadoId">ID do chamado</param>
        /// <param name="request">Dados da atualização</param>
        /// <returns>Chamado atualizado</returns>
        public static async Task<Chamado> AtualizarStatusAsync(int chamadoId, StatusUpdateRequest request)
        {
            return await ApiService.PutAsync<StatusUpdateRequest, Chamado>(
                ApiConfig.ChamadoStatusEndpoint(chamadoId),
                request
            );
        }

        /// <summary>
        /// Adiciona um comentário ao chamado.
        /// </summary>
        /// <param name="chamadoId">ID do chamado</param>
        /// <param name="request">Dados do comentário</param>
        /// <returns>Comentário criado</returns>
        public static async Task<Comentario> AdicionarComentarioAsync(int chamadoId, ComentarioRequest request)
        {
            return await ApiService.PostAsync<ComentarioRequest, Comentario>(
                ApiConfig.ChamadoComentariosEndpoint(chamadoId),
                request
            );
        }

        /// <summary>
        /// Lista os comentários de um chamado.
        /// </summary>
        /// <param name="chamadoId">ID do chamado</param>
        /// <returns>Lista de comentários</returns>
        public static async Task<List<Comentario>> ListarComentariosAsync(int chamadoId)
        {
            return await ApiService.GetAsync<List<Comentario>>(
                ApiConfig.ChamadoComentariosEndpoint(chamadoId)
            );
        }

        /// <summary>
        /// Obtém estatísticas dos chamados para o dashboard.
        /// </summary>
        /// <returns>Estatísticas dos chamados</returns>
        public static async Task<DashboardStats> ObterEstatisticasAsync()
        {
            return await ApiService.GetAsync<DashboardStats>(ApiConfig.ChamadosStatsEndpoint);
        }

        /// <summary>
        /// Solicita assistência da IA para análise de problema.
        /// </summary>
        /// <param name="descricao">Descrição do problema</param>
        /// <param name="categoria">Categoria (opcional)</param>
        /// <returns>Resposta da IA</returns>
        public static async Task<IAAssistResponse> SolicitarAssistenciaIAAsync(string descricao, string categoria = null)
        {
            var request = new IAAssistRequest
            {
                Descricao = descricao,
                Categoria = categoria
            };

            return await ApiService.PostAsync<IAAssistRequest, IAAssistResponse>(
                ApiConfig.IAAssistEndpoint,
                request
            );
        }

        /// <summary>
        /// Exclui um chamado (apenas admin).
        /// </summary>
        /// <param name="id">ID do chamado</param>
        /// <returns>Resposta da exclusão</returns>
        public static async Task<ApiResponse<object>> ExcluirAsync(int id)
        {
            return await ApiService.DeleteAsync<ApiResponse<object>>(ApiConfig.ChamadoEndpoint(id));
        }
    }
}
