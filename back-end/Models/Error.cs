namespace PFM.Models{
    public class Error{
        public Error(string tag,string message){
            this.tag=tag;
            this.message=message;
        }
        public string tag{set;get;}
        public string message {set;get;}
     
    }
}