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
            var categories=await _categoriesRepository.Get(parentId);
            
            return _mapper.Map<List<Category>>(categories);
        }

        public async Task<List<Category>> ImportCategories(List<Category> categories)
        {
            var cats=new List<Category>();
            foreach(Category category in categories){
                var posted=await _categoriesRepository.Import(_mapper.Map<CategoryEntity>(category));
                if(posted!=null)
                    cats.Add(_mapper.Map<Category>(posted));
            }
            return cats;
        }
    }
}