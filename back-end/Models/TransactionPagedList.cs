

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PFM.Models{
    public class TransactionPagedList<T>{
        [JsonPropertyName("total-count")]
        public int TotalCount{set;get;}
        [JsonPropertyName("page-size")]
        public int PageSize{set;get;}
        public int Page {set;get;}
        [JsonPropertyName("total-pages")]
        public int TotalPages {set;get;}
        [JsonPropertyName("sort-order")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SortOrder SortOrder{set;get;}
        [JsonPropertyName("sort-by")]
        public string SortBy{set;get;}
        public List<T> Items {set;get;}
        
    }
}