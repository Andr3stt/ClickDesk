using Newtonsoft.Json;

namespace ClickDesk.Models
{
    /// <summary>
    /// DTO para requisição de criação de chamado.
    /// Enviado para POST /api/chamados
    /// </summary>
    public class ChamadoRequest
    {
        /// <summary>
        /// Título do chamado
        /// </summary>
        [JsonProperty("titulo")]
        public string Titulo { get; set; }

        /// <summary>
        /// Descrição detalhada do problema
        /// </summary>
        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        /// <summary>
        /// Categoria do chamado
        /// </summary>
        [JsonProperty("categoria")]
        public string Categoria { get; set; }

        /// <summary>
        /// Severidade do chamado (opcional - pode ser calculada pela IA)
        /// </summary>
        [JsonProperty("severidade")]
        public string Severidade { get; set; }

        /// <summary>
        /// Prioridade do chamado (opcional)
        /// </summary>
        [JsonProperty("prioridade")]
        public string Prioridade { get; set; }

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public ChamadoRequest() { }

        /// <summary>
        /// Construtor com parâmetros básicos
        /// </summary>
        public ChamadoRequest(string titulo, string descricao, string categoria)
        {
            Titulo = titulo;
            Descricao = descricao;
            Categoria = categoria;
        }
    }

    /// <summary>
    /// DTO para envio de feedback sobre solução da IA
    /// </summary>
    public class FeedbackRequest
    {
        /// <summary>
        /// Indica se a solução da IA foi útil
        /// </summary>
        [JsonProperty("solucaoUtil")]
        public bool SolucaoUtil { get; set; }

        /// <summary>
        /// Avaliação de 1 a 5
        /// </summary>
        [JsonProperty("avaliacao")]
        public int Avaliacao { get; set; }

        /// <summary>
        /// Comentário adicional do usuário
        /// </summary>
        [JsonProperty("comentario")]
        public string Comentario { get; set; }

        /// <summary>
        /// Indica se deseja escalar para técnico humano
        /// </summary>
        [JsonProperty("escalarParaTecnico")]
        public bool EscalarParaTecnico { get; set; }
    }

    /// <summary>
    /// DTO para atualização de status do chamado
    /// </summary>
    public class StatusUpdateRequest
    {
        /// <summary>
        /// Novo status do chamado
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Motivo da alteração
        /// </summary>
        [JsonProperty("motivo")]
        public string Motivo { get; set; }

        /// <summary>
        /// ID do técnico (para atribuição)
        /// </summary>
        [JsonProperty("tecnicoId")]
        public int? TecnicoId { get; set; }
    }

    /// <summary>
    /// DTO para adicionar comentário ao chamado
    /// </summary>
    public class ComentarioRequest
    {
        /// <summary>
        /// Texto do comentário
        /// </summary>
        [JsonProperty("texto")]
        public string Texto { get; set; }

        /// <summary>
        /// Indica se é uma solução
        /// </summary>
        [JsonProperty("solucao")]
        public bool Solucao { get; set; }
    }

    /// <summary>
    /// DTO para requisição de assistência da IA
    /// </summary>
    public class IAAssistRequest
    {
        /// <summary>
        /// Descrição do problema para análise da IA
        /// </summary>
        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        /// <summary>
        /// Categoria para contexto (opcional)
        /// </summary>
        [JsonProperty("categoria")]
        public string Categoria { get; set; }
    }
}
