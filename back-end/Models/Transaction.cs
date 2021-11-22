using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using PFM.Database.Entities;

namespace PFM.Models{
    public class Transaction{
        //id,beneficiary-name,date,direction,amount,description,currency,mcc,kind
        public string Id {set;get;} 
    
        [JsonPropertyName("beneficiary-name")]
        public string BeneficiaryName {set;get;}
        [JsonPropertyName("cat-code")]
        public string CatCode {set;get;}

        public DateTime?  Date	{set;get;}
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Direction? Direction	{set;get;}
    
        public double? Amount{set;get;}
    
        public string Description	{set;get;}
    
        public string Currency	{set;get;}
    
        public int? Mcc	{set;get;}
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionKind? Kind{set;get;}
        public List<SplitTransaction> splits {set;get;}
    }
}