using System.Collections.Generic;
using System.Threading.Tasks;
using ClickDesk.Models;

namespace ClickDesk.Services.API
{
    /// <summary>
    /// Serviço para operações com FAQ (Perguntas Frequentes).
    /// Gerencia listagem, criação, edição e exclusão de FAQs.
    /// </summary>
    public static class FAQService
    {
        /// <summary>
        /// Lista todas as FAQs ativas.
        /// </summary>
        /// <returns>Lista de FAQs</returns>
        public static async Task<List<FAQ>> ListarAsync()
        {
            return await ApiService.GetAsync<List<FAQ>>(ApiConfig.FAQsEndpoint);
        }

        /// <summary>
        /// Obtém uma FAQ específica pelo ID.
        /// </summary>
        /// <param name="id">ID da FAQ</param>
        /// <returns>Dados da FAQ</returns>
        public static async Task<FAQ> ObterAsync(int id)
        {
            return await ApiService.GetAsync<FAQ>(ApiConfig.FAQEndpoint(id));
        }

        /// <summary>
        /// Busca FAQs por termo.
        /// </summary>
        /// <param name="termo">Termo de busca</param>
        /// <returns>Lista de FAQs correspondentes</returns>
        public static async Task<List<FAQ>> BuscarAsync(string termo)
        {
            string url = $"{ApiConfig.FAQSearchEndpoint}?q={System.Uri.EscapeDataString(termo)}";
            return await ApiService.GetAsync<List<FAQ>>(url);
        }

        /// <summary>
        /// Cria uma nova FAQ (admin).
        /// </summary>
        /// <param name="faq">Dados da FAQ</param>
        /// <returns>FAQ criada</returns>
        public static async Task<FAQ> CriarAsync(FAQ faq)
        {
            return await ApiService.PostAsync<FAQ, FAQ>(ApiConfig.FAQsEndpoint, faq);
        }

        /// <summary>
        /// Atualiza uma FAQ existente (admin).
        /// </summary>
        /// <param name="id">ID da FAQ</param>
        /// <param name="faq">Dados atualizados</param>
        /// <returns>FAQ atualizada</returns>
        public static async Task<FAQ> AtualizarAsync(int id, FAQ faq)
        {
            return await ApiService.PutAsync<FAQ, FAQ>(ApiConfig.FAQEndpoint(id), faq);
        }

        /// <summary>
        /// Exclui uma FAQ (admin).
        /// </summary>
        /// <param name="id">ID da FAQ</param>
        /// <returns>Resposta da exclusão</returns>
        public static async Task<ApiResponse<object>> ExcluirAsync(int id)
        {
            return await ApiService.DeleteAsync<ApiResponse<object>>(ApiConfig.FAQEndpoint(id));
        }
    }
}
