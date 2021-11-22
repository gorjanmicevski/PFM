 using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using PFM.Models;
using System;
using PFM.Database.Entities;
using pfm.Models;

namespace PFM.Services{
   public class CsvImportService : ICsvImportService
    {
        public List<Category> ImportCategoriesCsv(IFormFile formFile)
        {
            var categories=new List<Category>();
            using (var streamReader=new StreamReader(formFile.OpenReadStream())){
                using(var csvReader = new CsvReader(streamReader,CultureInfo.InvariantCulture)){
                    
                    var records = csvReader.GetRecords<dynamic>().ToList();
                   foreach(var record in records){
                        IDictionary<string, object> propertyValues =record;
                        var values=new List<String>();
                        
                    foreach (var property in propertyValues.Keys)
                    {
                        values.Add(propertyValues[property].ToString());
                    }
                Category category;
                    if(string.IsNullOrEmpty(values[1])){
                        category=new Category()
                            {Code=values[0],
                            Name=values[2]};
                    }else{
                         category=new Category()
                            {Code=values[0],
                            ParentCode=values[1],
                            Name=values[2]};
                    }
                        
                        categories.Add(category);
                    }      
                }
                    
            }
            //Animal animal = (Animal)Enum.Parse(typeof(Animal), str, true)
            return categories;
        
        }

        public List<TransactionEntity> ImportTransactionsCsv(IFormFile formFile)
        {
            var transactions=new List<TransactionEntity>();
            
            //var streamReader = new StreamReader(@"C:\Users\Instructor\Downloads\pfm-main\pfm-main\transactions.csv"
            using (var streamReader=new StreamReader(formFile.OpenReadStream())){
                using(var csvReader = new CsvReader(streamReader,CultureInfo.InvariantCulture)){
                    
                    var records = csvReader.GetRecords<dynamic>().ToList();
                    foreach(var record in records){
                    if(record.amount=="")
                    break;
                    string a=record.amount;
                    double amount;
                    if(a.Contains("\""))
                    {
                            a=a.Substring(1,a.Length);
                            var x=a.Split(",");
                            amount=(int.Parse(x[0]))*1000+Double.Parse(x[1]);
                    }
                        else
                        amount=Double.Parse(record.amount);
                    var date=DateTime.Parse(record.date);
                    IDictionary<string, object> propertyValues =record;
                    var values=new List<String>();
                    var i=0;
                foreach (var property in propertyValues.Keys)
                {
                    values.Add(propertyValues[property].ToString());
                    i++;
                    if(i==2)
                    break;
                } 
                    var direction=(Direction) Enum.Parse(typeof(Direction),record.direction,true);
                    var kind=(TransactionKind) Enum.Parse(typeof(TransactionKind),record.kind,true);
                    MCC? mcc;
                    if(record.mcc==""){
                        mcc=null;
                    }else
                    {
                       // mcc=int.Parse(record.mcc);
                       mcc=(MCC) Enum.Parse(typeof(MCC),record.mcc);
                    }
                    var transaction=new TransactionEntity(){Id=record.id,
                    BeneficiaryName=values[1],
                    Date=date,Direction=direction,Amount=amount,Description=record.description,
                    Currency=record.currency,Mcc=mcc,Kind=kind};
                    transactions.Add(transaction);
                }      
                    }
                    
            }
            //Animal animal = (Animal)Enum.Parse(typeof(Animal), str, true)
           return transactions;
        }
    }
}