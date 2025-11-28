using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ClickDesk.Models
{
    /// <summary>
    /// Modelo que representa um chamado/ticket no sistema.
    /// Contém todas as informações do chamado incluindo status e resolução por IA.
    /// </summary>
    public class Chamado
    {
        /// <summary>
        /// Identificador único do chamado
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Título descritivo do chamado
        /// </summary>
        [JsonProperty("titulo")]
        public string Titulo { get; set; }

        /// <summary>
        /// Descrição detalhada do problema
        /// </summary>
        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        /// <summary>
        /// Categoria do chamado (Hardware, Software, Rede, etc.)
        /// </summary>
        [JsonProperty("categoria")]
        public string Categoria { get; set; }

        /// <summary>
        /// Status atual do chamado (ABERTO, EM_ANDAMENTO, RESOLVIDO, FECHADO, ESCALADO)
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Severidade do chamado (BAIXA, MEDIA, ALTA, CRITICA)
        /// </summary>
        [JsonProperty("severidade")]
        public string Severidade { get; set; }

        /// <summary>
        /// Prioridade do chamado
        /// </summary>
        [JsonProperty("prioridade")]
        public string Prioridade { get; set; }

        /// <summary>
        /// ID do usuário que criou o chamado
        /// </summary>
        [JsonProperty("usuarioId")]
        public int UsuarioId { get; set; }

        /// <summary>
        /// Nome do usuário que criou o chamado
        /// </summary>
        [JsonProperty("usuarioNome")]
        public string UsuarioNome { get; set; }

        /// <summary>
        /// ID do técnico responsável
        /// </summary>
        [JsonProperty("tecnicoId")]
        public int? TecnicoId { get; set; }

        /// <summary>
        /// Nome do técnico responsável
        /// </summary>
        [JsonProperty("tecnicoNome")]
        public string TecnicoNome { get; set; }

        /// <summary>
        /// Data de abertura do chamado
        /// </summary>
        [JsonProperty("dataAbertura")]
        public DateTime DataAbertura { get; set; }

        /// <summary>
        /// Data de fechamento do chamado
        /// </summary>
        [JsonProperty("dataFechamento")]
        public DateTime? DataFechamento { get; set; }

        /// <summary>
        /// Data da última atualização
        /// </summary>
        [JsonProperty("dataAtualizacao")]
        public DateTime? DataAtualizacao { get; set; }

        /// <summary>
        /// Indica se foi resolvido pela IA
        /// </summary>
        [JsonProperty("resolvidoPorIA")]
        public bool ResolvidoPorIA { get; set; }

        /// <summary>
        /// Solução proposta pela IA
        /// </summary>
        [JsonProperty("solucaoIA")]
        public string SolucaoIA { get; set; }

        /// <summary>
        /// Feedback do usuário sobre a solução da IA
        /// </summary>
        [JsonProperty("feedbackIA")]
        public string FeedbackIA { get; set; }

        /// <summary>
        /// Avaliação do usuário (1-5)
        /// </summary>
        [JsonProperty("avaliacao")]
        public int? Avaliacao { get; set; }

        /// <summary>
        /// Lista de comentários do chamado
        /// </summary>
        [JsonProperty("comentarios")]
        public List<Comentario> Comentarios { get; set; }

        /// <summary>
        /// Lista de arquivos anexados
        /// </summary>
        [JsonProperty("anexos")]
        public List<Anexo> Anexos { get; set; }

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public Chamado()
        {
            Comentarios = new List<Comentario>();
            Anexos = new List<Anexo>();
        }

        /// <summary>
        /// Retorna a cor associada ao status para exibição na UI
        /// </summary>
        public string StatusColor
        {
            get
            {
                switch (Status?.ToUpper())
                {
                    case "ABERTO": return "#2563eb"; // Azul
                    case "EM_ANDAMENTO": return "#f59e0b"; // Amarelo
                    case "RESOLVIDO": return "#10b981"; // Verde
                    case "FECHADO": return "#6b7280"; // Cinza
                    case "ESCALADO": return "#ef4444"; // Vermelho
                    default: return "#6b7280";
                }
            }
        }

        /// <summary>
        /// Retorna a cor associada à severidade para exibição na UI
        /// </summary>
        public string SeveridadeColor
        {
            get
            {
                switch (Severidade?.ToUpper())
                {
                    case "BAIXA": return "#10b981"; // Verde
                    case "MEDIA": return "#f59e0b"; // Amarelo
                    case "ALTA": return "#ef4444"; // Vermelho
                    case "CRITICA": return "#7c2d12"; // Vermelho escuro
                    default: return "#6b7280";
                }
            }
        }
    }

    /// <summary>
    /// Modelo que representa um comentário em um chamado
    /// </summary>
    public class Comentario
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("chamadoId")]
        public int ChamadoId { get; set; }

        [JsonProperty("usuarioId")]
        public int UsuarioId { get; set; }

        [JsonProperty("usuarioNome")]
        public string UsuarioNome { get; set; }

        [JsonProperty("texto")]
        public string Texto { get; set; }

        [JsonProperty("dataCriacao")]
        public DateTime DataCriacao { get; set; }
    }

    /// <summary>
    /// Modelo que representa um anexo de um chamado
    /// </summary>
    public class Anexo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("chamadoId")]
        public int ChamadoId { get; set; }

        [JsonProperty("nomeArquivo")]
        public string NomeArquivo { get; set; }

        [JsonProperty("tipoArquivo")]
        public string TipoArquivo { get; set; }

        [JsonProperty("tamanho")]
        public long Tamanho { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("dataUpload")]
        public DateTime DataUpload { get; set; }
    }
}
