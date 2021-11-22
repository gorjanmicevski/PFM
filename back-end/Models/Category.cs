
using System.Text.Json.Serialization;

namespace PFM.Models{
    public class Category{
        public string Code {set;get;} 
        [JsonPropertyName("parent-code")]
        public string ParentCode {set;get;}
        public string Name {set;get;}
    }
}