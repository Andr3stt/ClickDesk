using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ClickDesk.Utils;

namespace ClickDesk.Services.API
{
    /// <summary>
    /// Serviço genérico para comunicação com a API REST.
    /// Encapsula todas as operações HTTP (GET, POST, PUT, DELETE).
    /// </summary>
    public class ApiService
    {
        // Cliente HTTP reutilizável para todas as requisições
        private static readonly HttpClient _httpClient;

        /// <summary>
        /// Construtor estático - inicializa o HttpClient uma única vez
        /// </summary>
        static ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(ApiConfig.DefaultTimeout);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Configura o token JWT no header de autorização.
        /// Deve ser chamado após o login bem-sucedido.
        /// </summary>
        /// <param name="token">Token JWT</param>
        public static void SetAuthToken(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        /// <summary>
        /// Remove o token de autorização (para logout)
        /// </summary>
        public static void ClearAuthToken()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        /// <summary>
        /// Executa uma requisição GET.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto esperado na resposta</typeparam>
        /// <param name="url">URL completa da requisição</param>
        /// <returns>Objeto deserializado do tipo T</returns>
        public static async Task<T> GetAsync<T>(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                return await ProcessResponse<T>(response);
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException("Erro de conexão com o servidor. Verifique sua conexão.", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new ApiException("Tempo limite da requisição excedido.", ex);
            }
        }

        /// <summary>
        /// Executa uma requisição POST com corpo JSON.
        /// </summary>
        /// <typeparam name="TRequest">Tipo do objeto enviado</typeparam>
        /// <typeparam name="TResponse">Tipo do objeto esperado na resposta</typeparam>
        /// <param name="url">URL completa da requisição</param>
        /// <param name="data">Objeto a ser enviado no corpo da requisição</param>
        /// <returns>Objeto deserializado do tipo TResponse</returns>
        public static async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest data)
        {
            try
            {
                string json = JsonConvert.SerializeObject(data);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = await _httpClient.PostAsync(url, content);
                return await ProcessResponse<TResponse>(response);
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException("Erro de conexão com o servidor. Verifique sua conexão.", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new ApiException("Tempo limite da requisição excedido.", ex);
            }
        }

        /// <summary>
        /// Executa uma requisição POST simples (sem corpo).
        /// </summary>
        public static async Task<T> PostAsync<T>(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync(url, null);
                return await ProcessResponse<T>(response);
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException("Erro de conexão com o servidor. Verifique sua conexão.", ex);
            }
        }

        /// <summary>
        /// Executa uma requisição PUT com corpo JSON.
        /// </summary>
        public static async Task<TResponse> PutAsync<TRequest, TResponse>(string url, TRequest data)
        {
            try
            {
                string json = JsonConvert.SerializeObject(data);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = await _httpClient.PutAsync(url, content);
                return await ProcessResponse<TResponse>(response);
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException("Erro de conexão com o servidor. Verifique sua conexão.", ex);
            }
        }

        /// <summary>
        /// Executa uma requisição DELETE.
        /// </summary>
        public static async Task<T> DeleteAsync<T>(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync(url);
                return await ProcessResponse<T>(response);
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException("Erro de conexão com o servidor. Verifique sua conexão.", ex);
            }
        }

        /// <summary>
        /// Processa a resposta HTTP e deserializa o JSON.
        /// </summary>
        private static async Task<T> ProcessResponse<T>(HttpResponseMessage response)
        {
            string content = await response.Content.ReadAsStringAsync();

            // Verifica o status code
            if (!response.IsSuccessStatusCode)
            {
                // Tenta extrair mensagem de erro do JSON
                string errorMessage;
                try
                {
                    var errorObj = JsonConvert.DeserializeObject<ErrorResponse>(content);
                    errorMessage = errorObj?.Message ?? errorObj?.Error ?? content;
                }
                catch
                {
                    errorMessage = content;
                }

                // Trata códigos específicos
                switch ((int)response.StatusCode)
                {
                    case 401:
                        throw new ApiException("Sessão expirada. Faça login novamente.", null, 401);
                    case 403:
                        throw new ApiException("Você não tem permissão para esta operação.", null, 403);
                    case 404:
                        throw new ApiException("Recurso não encontrado.", null, 404);
                    case 400:
                        throw new ApiException(errorMessage ?? "Dados inválidos.", null, 400);
                    case 500:
                        throw new ApiException("Erro interno do servidor. Tente novamente.", null, 500);
                    default:
                        throw new ApiException(errorMessage ?? $"Erro: {response.StatusCode}", null, (int)response.StatusCode);
                }
            }

            // Deserializa resposta bem-sucedida
            if (string.IsNullOrEmpty(content))
            {
                return default(T);
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (JsonException ex)
            {
                throw new ApiException("Erro ao processar resposta do servidor.", ex);
            }
        }

        /// <summary>
        /// Classe auxiliar para deserializar erros da API
        /// </summary>
        private class ErrorResponse
        {
            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("error")]
            public string Error { get; set; }
        }
    }

    /// <summary>
    /// Exceção customizada para erros de API.
    /// Permite tratamento específico de erros de comunicação.
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// Código HTTP do erro (quando disponível)
        /// </summary>
        public int StatusCode { get; }

        public ApiException(string message, Exception innerException = null, int statusCode = 0) 
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
