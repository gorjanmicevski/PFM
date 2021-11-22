using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PFM.Database.Entities;
using PFM.Models;
using PFM.Repositories;

namespace PFM.Services{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;

        public CategoriesService(ICategoriesRepository repository,IMapper mapper){
            _categoriesRepository=repository;
            _mapper=mapper;
        }
        public async Task<List<Category>> Get(string parentId)
        {
            if(parentId!=null){
            var check=await _categoriesRepository.GetCategory(parentId);
            if(check==null)
            throw new ErrorException(new Error("category","category with code "+parentId+" does not exist"));
            }
            var categories=await _categoriesRepository.Get(parentId);
            
            return _mapper.Map<List<Category>>(categories);
        }

        public async Task ImportCategories(List<Category> categories)
        {
           
            foreach(Category category in categories){
               await _categoriesRepository.Import(_mapper.Map<CategoryEntity>(category)); 
            }
        }
    }
}