using System.Threading.Tasks;
using ClickDesk.Models;

namespace ClickDesk.Services.API
{
    /// <summary>
    /// Serviço para operações de assistência com IA.
    /// Gerencia requisições de análise de problemas pela IA.
    /// </summary>
    public static class IAService
    {
        /// <summary>
        /// Solicita assistência da IA para análise de um problema.
        /// </summary>
        /// <param name="prompt">Descrição do problema/pergunta</param>
        /// <returns>Resposta da IA com análise e recomendações</returns>
        public static async Task<ApiResponse<IAResult>> AssistAsync(string prompt)
        {
            var request = new
            {
                prompt = prompt
            };

            return await ApiService.PostAsync<object, ApiResponse<IAResult>>(
                ApiConfig.IAAssistEndpoint,
                request
            );
        }
    }
}
