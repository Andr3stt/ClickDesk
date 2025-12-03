using System.Collections.Generic;
using Newtonsoft.Json;

namespace ClickDesk.Models
{
    /// <summary>
    /// Modelo genérico para respostas da API.
    /// Encapsula o resultado de qualquer operação com a API REST.
    /// </summary>
    /// <typeparam name="T">Tipo de dado retornado no campo Data</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Indica se a operação foi bem-sucedida
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Mensagem de retorno (sucesso ou erro)
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Dados retornados pela operação
        /// </summary>
        [JsonProperty("data")]
        public T Data { get; set; }

        /// <summary>
        /// Código de erro (quando aplicável)
        /// </summary>
        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }
    }

    /// <summary>
    /// Resposta específica para operações de autenticação.
    /// Contém o token JWT e informações do usuário.
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// Token JWT para autenticação
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }

        /// <summary>
        /// Tipo do token (geralmente "Bearer")
        /// </summary>
        [JsonProperty("tokenType")]
        public string TokenType { get; set; }

        /// <summary>
        /// Tempo de expiração em segundos
        /// </summary>
        [JsonProperty("expiresIn")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Nome de usuário
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// Roles/permissões do usuário
        /// </summary>
        [JsonProperty("roles")]
        public List<string> Roles { get; set; }

        /// <summary>
        /// Dados do usuário autenticado
        /// </summary>
        [JsonProperty("user")]
        public User User { get; set; }
    }

    /// <summary>
    /// Resposta para operações com chamados que podem ser resolvidos por IA.
    /// </summary>
    public class ChamadoResponse
    {
        /// <summary>
        /// Dados do chamado criado/atualizado
        /// </summary>
        [JsonProperty("chamado")]
        public Chamado Chamado { get; set; }

        /// <summary>
        /// Indica se foi resolvido automaticamente pela IA
        /// </summary>
        [JsonProperty("resolvidoPorIA")]
        public bool ResolvidoPorIA { get; set; }

        /// <summary>
        /// Solução sugerida pela IA
        /// </summary>
        [JsonProperty("solucaoIA")]
        public string SolucaoIA { get; set; }

        /// <summary>
        /// Mensagem adicional sobre o processamento
        /// </summary>
        [JsonProperty("mensagem")]
        public string Mensagem { get; set; }

        /// <summary>
        /// Indica se precisa de feedback do usuário
        /// </summary>
        [JsonProperty("aguardandoFeedback")]
        public bool AguardandoFeedback { get; set; }
    }

    /// <summary>
    /// Resposta para estatísticas do dashboard
    /// </summary>
    public class DashboardStats
    {
        /// <summary>
        /// Total de chamados
        /// </summary>
        [JsonProperty("totalChamados")]
        public int TotalChamados { get; set; }

        /// <summary>
        /// Chamados em aberto
        /// </summary>
        [JsonProperty("chamadosAbertos")]
        public int ChamadosAbertos { get; set; }

        /// <summary>
        /// Chamados em andamento
        /// </summary>
        [JsonProperty("chamadosEmAndamento")]
        public int ChamadosEmAndamento { get; set; }

        /// <summary>
        /// Chamados resolvidos
        /// </summary>
        [JsonProperty("chamadosResolvidos")]
        public int ChamadosResolvidos { get; set; }

        /// <summary>
        /// Chamados escalados
        /// </summary>
        [JsonProperty("chamadosEscalados")]
        public int ChamadosEscalados { get; set; }

        /// <summary>
        /// Chamados resolvidos por IA
        /// </summary>
        [JsonProperty("resolvidosPorIA")]
        public int ResolvidosPorIA { get; set; }

        /// <summary>
        /// Tempo médio de resolução em minutos
        /// </summary>
        [JsonProperty("tempoMedioResolucao")]
        public double TempoMedioResolucao { get; set; }

        /// <summary>
        /// Taxa de satisfação (0-100%)
        /// </summary>
        [JsonProperty("taxaSatisfacao")]
        public double TaxaSatisfacao { get; set; }
    }

    /// <summary>
    /// Resposta da assistência da IA
    /// </summary>
    public class IAAssistResponse
    {
        /// <summary>
        /// Indica se a IA conseguiu resolver
        /// </summary>
        [JsonProperty("resolvido")]
        public bool Resolvido { get; set; }

        /// <summary>
        /// Solução proposta pela IA
        /// </summary>
        [JsonProperty("solucao")]
        public string Solucao { get; set; }

        /// <summary>
        /// Confiança da solução (0-100%)
        /// </summary>
        [JsonProperty("confianca")]
        public double Confianca { get; set; }

        /// <summary>
        /// FAQs relacionadas
        /// </summary>
        [JsonProperty("faqsRelacionadas")]
        public FAQ[] FAQsRelacionadas { get; set; }

        /// <summary>
        /// Categoria sugerida
        /// </summary>
        [JsonProperty("categoriaSugerida")]
        public string CategoriaSugerida { get; set; }

        /// <summary>
        /// Severidade sugerida
        /// </summary>
        [JsonProperty("severidadeSugerida")]
        public string SeveridadeSugerida { get; set; }
    }
}
