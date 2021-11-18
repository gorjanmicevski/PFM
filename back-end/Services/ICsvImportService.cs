using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using PFM.Models;

namespace PFM.Services{
    public interface ICsvImportService{
        List<Transaction> ImportTransactionsCsv(IFormFile formFile); 
        List<Category> ImportCategoriesCsv(IFormFile formFile); 
    }
}