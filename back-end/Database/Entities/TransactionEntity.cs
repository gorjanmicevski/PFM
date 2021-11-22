using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PFM.Models;

namespace PFM.Database.Entities{
    public class TransactionEntity{

        [Required]
        public string Id {set;get;}
        
        public string BeneficiaryName {set;get;}
        public string CatCode {set;get;}
        [Required]
        public DateTime  Date	{set;get;}
        [Required]
        public Direction Direction	{set;get;}
        [Required]
        public double Amount{set;get;}
        public string Description	{set;get;}
        [Required]
        [MaxLength(3),MinLength(3)]
        public string Currency	{set;get;}
        public int? Mcc	{set;get;}
        [Required]
        public TransactionKind Kind{set;get;}
        public CategoryEntity Category {set;get;}

        public List<SplitTransactionEntity> splits{set;get;}
    }
}