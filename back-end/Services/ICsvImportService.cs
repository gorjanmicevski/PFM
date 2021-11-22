using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using PFM.Database.Entities;
using PFM.Models;

namespace PFM.Services{
    public interface ICsvImportService{
        List<TransactionEntity> ImportTransactionsCsv(IFormFile formFile); 
        List<Category> ImportCategoriesCsv(IFormFile formFile); 
    }
}