using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PFM.Database;
using PFM.Database.Entities;

namespace PFM.Repositories{
    class CategoriesRepository : ICategoriesRepository
    {
        private readonly TransactionsDbContext _dbContext;
        public CategoriesRepository(TransactionsDbContext dbContext){
            _dbContext=dbContext;
        }
        public async Task<List<CategoryEntity>> Get(string parentId)
        {
            var query= _dbContext.Categories.AsQueryable();
            query=query.Where(x=>parentId==x.ParentCode);
            return await query.ToListAsync();
        }

        public async Task<CategoryEntity> GetCategory(string catcode)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c=>c.Code==catcode);
        }

        public async Task<CategoryEntity> Import(CategoryEntity categoryEntity)
        {
            var check=await _dbContext.Categories.FirstOrDefaultAsync(c=>c.Code==categoryEntity.Code);
            if(check==null){
                if(categoryEntity.ParentCode!=null){
                    var parent=await _dbContext.Categories.FirstOrDefaultAsync(c=>c.Code==categoryEntity.ParentCode);
                    if(parent!=null){
                       categoryEntity.ParentCat=parent;
                       if(parent.ChildCat==null)
                        parent.ChildCat=new List<CategoryEntity>();
                       parent.ChildCat.Add(categoryEntity);
                        _dbContext.Categories.Update(parent);         
                    }
                }
                await _dbContext.Categories.AddAsync(categoryEntity);
            }
            
            else
            {   
                check.Name=categoryEntity.Name;
                if(categoryEntity.ParentCode !=null)
                check.ParentCode=categoryEntity.ParentCode;
                _dbContext.Categories.Update(check);                    
            }
            await _dbContext.SaveChangesAsync();
            return categoryEntity;
        }
    }
}