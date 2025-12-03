using System;
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
        public static async Task<List<FAQ>> ListAsync()
        {
            return await ApiService.GetAsync<List<FAQ>>(ApiConfig.FAQsEndpoint);
        }

        /// <summary>
        /// Obtém uma FAQ específica pelo ID.
        /// </summary>
        /// <param name="id">ID da FAQ</param>
        /// <returns>Dados da FAQ</returns>
        public static async Task<FAQ> GetByIdAsync(int id)
        {
            return await ApiService.GetAsync<FAQ>(ApiConfig.FAQEndpoint(id));
        }

        /// <summary>
        /// Busca FAQs por termo (keyword).
        /// </summary>
        /// <param name="keyword">Termo de busca</param>
        /// <returns>Lista de FAQs correspondentes</returns>
        public static async Task<List<FAQ>> SearchAsync(string keyword)
        {
            string url = $"{ApiConfig.FAQSearchEndpoint}?q={Uri.EscapeDataString(keyword)}";
            return await ApiService.GetAsync<List<FAQ>>(url);
        }

        /// <summary>
        /// Cria uma nova FAQ (admin).
        /// </summary>
        /// <param name="faq">Dados da FAQ</param>
        /// <returns>FAQ criada</returns>
        public static async Task<FAQ> CreateAsync(FAQ faq)
        {
            return await ApiService.PostAsync<FAQ, FAQ>(ApiConfig.FAQsEndpoint, faq);
        }

        /// <summary>
        /// Atualiza uma FAQ existente (admin).
        /// </summary>
        /// <param name="id">ID da FAQ</param>
        /// <param name="faq">Dados atualizados</param>
        /// <returns>FAQ atualizada</returns>
        public static async Task<FAQ> UpdateAsync(int id, FAQ faq)
        {
            return await ApiService.PutAsync<FAQ, FAQ>(ApiConfig.FAQEndpoint(id), faq);
        }

        /// <summary>
        /// Exclui uma FAQ (admin).
        /// </summary>
        /// <param name="id">ID da FAQ</param>
        /// <returns>Resposta da exclusão</returns>
        public static async Task<ApiResponse<object>> DeleteAsync(int id)
        {
            return await ApiService.DeleteAsync<ApiResponse<object>>(ApiConfig.FAQEndpoint(id));
        }
    }
}
