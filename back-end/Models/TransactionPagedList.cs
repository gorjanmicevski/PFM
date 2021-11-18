

using System.Collections.Generic;

namespace PFM.Models{
    public class TransactionPagedList<T>{
        public int TotalCount{set;get;}
        public int PageSize{set;get;}
        public int Page {set;get;}
        public int TotalPages {set;get;}
        public SortOrder SortOrder{set;get;}
        public string SortBy{set;get;}
        public List<T> Items {set;get;}
        
    }
}