using System;
using Newtonsoft.Json;

namespace ClickDesk.Models
{
    /// <summary>
    /// Modelo que representa um usuário do sistema.
    /// Contém todas as informações do usuário autenticado.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Identificador único do usuário
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Nome de usuário para login
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// Email do usuário
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Nome completo do usuário
        /// </summary>
        [JsonProperty("nome")]
        public string Nome { get; set; }

        /// <summary>
        /// Papel/função do usuário no sistema (USER, TECH, ADMIN)
        /// </summary>
        [JsonProperty("role")]
        public string Role { get; set; }

        /// <summary>
        /// Departamento do usuário
        /// </summary>
        [JsonProperty("departamento")]
        public string Departamento { get; set; }

        /// <summary>
        /// Telefone de contato
        /// </summary>
        [JsonProperty("telefone")]
        public string Telefone { get; set; }

        /// <summary>
        /// URL da foto de perfil
        /// </summary>
        [JsonProperty("fotoPerfil")]
        public string FotoPerfil { get; set; }

        /// <summary>
        /// Data de criação da conta
        /// </summary>
        [JsonProperty("dataCriacao")]
        public DateTime? DataCriacao { get; set; }

        /// <summary>
        /// Indica se o usuário está ativo
        /// </summary>
        [JsonProperty("ativo")]
        public bool Ativo { get; set; }

        /// <summary>
        /// Verifica se o usuário é administrador
        /// </summary>
        public bool IsAdmin => Role?.ToUpper() == "ADMIN";

        /// <summary>
        /// Verifica se o usuário é técnico
        /// </summary>
        public bool IsTech => Role?.ToUpper() == "TECH";

        /// <summary>
        /// Verifica se o usuário é usuário comum
        /// </summary>
        public bool IsUser => Role?.ToUpper() == "USER";

        /// <summary>
        /// Verifica se tem permissões administrativas (Admin ou Tech)
        /// </summary>
        public bool HasAdminAccess => IsAdmin || IsTech;
    }
}
