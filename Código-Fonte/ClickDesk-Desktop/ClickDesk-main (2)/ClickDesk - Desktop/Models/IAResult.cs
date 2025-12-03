using Newtonsoft.Json;

namespace ClickDesk.Models
{
    public class IAResult
    {
        [JsonProperty("resolved")]
        public bool Resolved { get; set; }

        [JsonProperty("confidence")]
        public double Confidence { get; set; }

        [JsonProperty("answer")]
        public string Answer { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("faqId")]
        public long? FaqId { get; set; }
    }
}
