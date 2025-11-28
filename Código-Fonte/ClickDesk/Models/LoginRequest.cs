using Newtonsoft.Json;

namespace ClickDesk.Models
{
    /// <summary>
    /// DTO para requisição de login.
    /// Enviado para POST /auth/login
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Nome de usuário ou email
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// Senha do usuário
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// Construtor padrão
        /// </summary>
        public LoginRequest() { }

        /// <summary>
        /// Construtor com parâmetros
        /// </summary>
        /// <param name="username">Nome de usuário</param>
        /// <param name="password">Senha</param>
        public LoginRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
