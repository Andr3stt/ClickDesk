using Newtonsoft.Json;

namespace ClickDesk.Models
{
    /// <summary>
    /// DTO para requisição de registro de novo usuário.
    /// Enviado para POST /auth/register
    /// </summary>
    public class RegisterRequest
    {
        /// <summary>
        /// Nome de usuário (único)
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// Email do usuário (único)
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Senha do usuário
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// Confirmação da senha
        /// </summary>
        [JsonProperty("confirmPassword")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Nome completo do usuário
        /// </summary>
        [JsonProperty("nome")]
        public string Nome { get; set; }

        /// <summary>
        /// Sobrenome do usuário
        /// </summary>
        [JsonProperty("sobrenome")]
        public string Sobrenome { get; set; }

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
        /// Papel do usuário (padrão: USER)
        /// </summary>
        [JsonProperty("role")]
        public string Role { get; set; }

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public RegisterRequest()
        {
            Role = "USER"; // Papel padrão
        }
    }
}
