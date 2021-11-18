using System.Collections.Generic;
using System.Threading.Tasks;
using PFM.Database.Entities;
using PFM.Models;

namespace PFM.Services{
    public interface ICategoriesService{
        Task<List<Category>> Get(string parentId);
        Task<List<Category>> ImportCategories(List<Category> categories);
    }
}