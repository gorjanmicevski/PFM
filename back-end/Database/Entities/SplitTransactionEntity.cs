
namespace PFM.Database.Entities{
    public class SplitTransactionEntity{
        //public string Id{set;get;}
        public string CatCode {set;get;}
        public double Amount {set;get;}
        public string TransactionId {set;get;}

        public TransactionEntity Transaction{set;get;}
        public CategoryEntity Category {set;get;}
    }
}