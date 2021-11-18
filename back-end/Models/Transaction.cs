using System;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace PFM.Models{
    public class Transaction{
        //id,beneficiary-name,date,direction,amount,description,currency,mcc,kind
        public string Id {set;get;} 
    
        public string BeneficiaryName {set;get;}
        public string CatCode {set;get;}
    
        public DateTime?  Date	{set;get;}
    
        public Direction? Direction	{set;get;}
    
        public double? Amount{set;get;}
    
        public string Description	{set;get;}
    
        public string Currency	{set;get;}
    
        public int? Mcc	{set;get;}
    
        public TransactionKind? Kind{set;get;}
    }
}