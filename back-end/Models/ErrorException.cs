using System;

namespace PFM.Models{
    public class ErrorException : Exception{
        public Error Error {set;get;}
        public ErrorException(Error error){
            this.Error=error;    
        }
    }
}