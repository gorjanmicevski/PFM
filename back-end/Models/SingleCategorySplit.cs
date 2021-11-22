using System.Text.Json.Serialization;

namespace PFM.Models{
    public class SingleCategorySplit{
        [JsonPropertyName("cat-code")]
        public string catcode{get;set;}
        public double amount {get;set;}
    }
}