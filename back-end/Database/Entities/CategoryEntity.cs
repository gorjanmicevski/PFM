using System.Collections.Generic;
using PFM.Models;

namespace PFM.Database.Entities{
    public class CategoryEntity{
        public string Code {set;get;} 
        public string ParentCode {set;get;}
        public string Name {set;get;}
        public CategoryEntity ParentCat{set;get;}
        public List<CategoryEntity> ChildCat{set;get;}
    }
}