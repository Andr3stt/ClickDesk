using Newtonsoft.Json;

namespace ClickDesk.Models
{
    public class VerifyEmailResponse
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("setPasswordToken")]
        public string SetPasswordToken { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
