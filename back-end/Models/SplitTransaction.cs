using System.Text.Json.Serialization;

namespace PFM.Models{
    public class SplitTransaction{
        [JsonPropertyName("cat-code")]
        public string CatCode {set;get;}
        public double Amount {set;get;}
    }
}