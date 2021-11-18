using System;
using System.Collections.Generic;
using PFM.Models;

namespace PFM.Database.Entities{
    public class TransactionEntity{

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
        public CategoryEntity Category {set;get;}

        public List<SplitTransactionEntity> splits{set;get;}
    }
}