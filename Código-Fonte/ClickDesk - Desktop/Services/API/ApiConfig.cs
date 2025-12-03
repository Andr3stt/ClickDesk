using System.Configuration;

namespace ClickDesk.Services.API
{
    /// <summary>
    /// Classe de configuração da API.
    /// Gerencia as URLs e configurações de conexão com o servidor.
    /// </summary>
    public static class ApiConfig
    {
        /// <summary>
        /// URL base da API REST.
        /// Configurável via App.config (chave: ApiBaseUrl)
        /// </summary>
        public static string BaseUrl
        {
            get
            {
                // Tenta ler do App.config, senão usa valor padrão
                string url = ConfigurationManager.AppSettings["ApiBaseUrl"];
                return string.IsNullOrEmpty(url) ? "http://localhost:8080" : url;
            }
        }

        // ========================================
        // ENDPOINTS DE AUTENTICAÇÃO
        // ========================================

        /// <summary>
        /// Endpoint para login (POST)
        /// </summary>
        public static string LoginEndpoint => $"{BaseUrl}/auth/login";

        /// <summary>
        /// Endpoint para registro (POST)
        /// </summary>
        public static string RegisterEndpoint => $"{BaseUrl}/auth/register";

        /// <summary>
        /// Endpoint para logout (POST)
        /// </summary>
        public static string LogoutEndpoint => $"{BaseUrl}/auth/logout";

        /// <summary>
        /// Endpoint para refresh token (POST)
        /// </summary>
        public static string RefreshTokenEndpoint => $"{BaseUrl}/auth/refresh";

        // ========================================
        // ENDPOINTS DE CHAMADOS
        // ========================================

        /// <summary>
        /// Endpoint para chamados (GET, POST)
        /// </summary>
        public static string ChamadosEndpoint => $"{BaseUrl}/api/chamados";

        /// <summary>
        /// Endpoint para um chamado específico (GET, PUT, DELETE)
        /// </summary>
        public static string ChamadoEndpoint(int id) => $"{BaseUrl}/api/chamados/{id}";

        /// <summary>
        /// Endpoint para feedback de chamado (POST)
        /// </summary>
        public static string ChamadoFeedbackEndpoint(int id) => $"{BaseUrl}/api/chamados/{id}/feedback";

        /// <summary>
        /// Endpoint para comentários do chamado (GET, POST)
        /// </summary>
        public static string ChamadoComentariosEndpoint(int id) => $"{BaseUrl}/api/chamados/{id}/comentarios";

        /// <summary>
        /// Endpoint para atualizar status do chamado (PUT)
        /// </summary>
        public static string ChamadoStatusEndpoint(int id) => $"{BaseUrl}/api/chamados/{id}/status";

        /// <summary>
        /// Endpoint para chamados do usuário logado (GET)
        /// </summary>
        public static string MeusChamadosEndpoint => $"{BaseUrl}/api/chamados/meus";

        /// <summary>
        /// Endpoint para estatísticas de chamados (GET)
        /// </summary>
        public static string ChamadosStatsEndpoint => $"{BaseUrl}/api/chamados/stats";

        // ========================================
        // ENDPOINTS DE FAQ
        // ========================================

        /// <summary>
        /// Endpoint para FAQs (GET, POST)
        /// </summary>
        public static string FAQsEndpoint => $"{BaseUrl}/api/faqs";

        /// <summary>
        /// Endpoint para uma FAQ específica (GET, PUT, DELETE)
        /// </summary>
        public static string FAQEndpoint(int id) => $"{BaseUrl}/api/faqs/{id}";

        /// <summary>
        /// Endpoint para buscar FAQs (GET)
        /// </summary>
        public static string FAQSearchEndpoint => $"{BaseUrl}/api/faqs/search";

        // ========================================
        // ENDPOINTS DE IA
        // ========================================

        /// <summary>
        /// Endpoint para assistência da IA (POST)
        /// </summary>
        public static string IAAssistEndpoint => $"{BaseUrl}/api/ia/assist";

        // ========================================
        // ENDPOINTS DE USUÁRIOS
        // ========================================

        /// <summary>
        /// Endpoint para usuários (GET, POST - admin)
        /// </summary>
        public static string UsersEndpoint => $"{BaseUrl}/api/users";

        /// <summary>
        /// Endpoint para um usuário específico (GET, PUT, DELETE)
        /// </summary>
        public static string UserEndpoint(int id) => $"{BaseUrl}/api/users/{id}";

        /// <summary>
        /// Endpoint para perfil do usuário logado (GET, PUT)
        /// </summary>
        public static string ProfileEndpoint => $"{BaseUrl}/api/users/profile";

        /// <summary>
        /// Endpoint para alterar senha (PUT)
        /// </summary>
        public static string ChangePasswordEndpoint => $"{BaseUrl}/api/users/change-password";

        // ========================================
        // CONFIGURAÇÕES DE TIMEOUT
        // ========================================

        /// <summary>
        /// Timeout padrão para requisições em segundos
        /// </summary>
        public static int DefaultTimeout => 30;

        /// <summary>
        /// Timeout para upload de arquivos em segundos
        /// </summary>
        public static int UploadTimeout => 120;
    }
}
