using System.Text.Json.Serialization;

namespace PFM.Commands{
    public class TransactionCategorizeCommand{
        [JsonPropertyName("cat-code")]
        public string catcode{get;set;}
    }
}