using System;
using Newtonsoft.Json;

namespace ClickDesk.Models
{
    /// <summary>
    /// Modelo que representa um item da FAQ (Perguntas Frequentes).
    /// Base de conhecimento para usuários e resolução automática.
    /// </summary>
    public class FAQ
    {
        /// <summary>
        /// Identificador único da FAQ
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Pergunta/título da FAQ
        /// </summary>
        [JsonProperty("pergunta")]
        public string Pergunta { get; set; }

        /// <summary>
        /// Resposta/conteúdo da FAQ
        /// </summary>
        [JsonProperty("resposta")]
        public string Resposta { get; set; }

        /// <summary>
        /// Categoria da FAQ
        /// </summary>
        [JsonProperty("categoria")]
        public string Categoria { get; set; }

        /// <summary>
        /// Palavras-chave para busca
        /// </summary>
        [JsonProperty("palavrasChave")]
        public string PalavrasChave { get; set; }

        /// <summary>
        /// Número de visualizações
        /// </summary>
        [JsonProperty("visualizacoes")]
        public int Visualizacoes { get; set; }

        /// <summary>
        /// Indica se está ativa e visível
        /// </summary>
        [JsonProperty("ativa")]
        public bool Ativa { get; set; }

        /// <summary>
        /// Data de criação
        /// </summary>
        [JsonProperty("dataCriacao")]
        public DateTime DataCriacao { get; set; }

        /// <summary>
        /// Data da última atualização
        /// </summary>
        [JsonProperty("dataAtualizacao")]
        public DateTime? DataAtualizacao { get; set; }

        /// <summary>
        /// ID do usuário que criou
        /// </summary>
        [JsonProperty("criadoPorId")]
        public int CriadoPorId { get; set; }

        /// <summary>
        /// Nome do usuário que criou
        /// </summary>
        [JsonProperty("criadoPorNome")]
        public string CriadoPorNome { get; set; }
    }
}
