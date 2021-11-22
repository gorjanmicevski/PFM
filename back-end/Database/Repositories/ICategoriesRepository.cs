using System.Collections.Generic;
using System.Threading.Tasks;
using PFM.Database.Entities;

namespace PFM.Repositories{
    public interface ICategoriesRepository{
        Task<List<CategoryEntity>> Get( string parentId);
        Task Import(CategoryEntity ca);
        Task<CategoryEntity> GetCategory(string catcode);
    }
}