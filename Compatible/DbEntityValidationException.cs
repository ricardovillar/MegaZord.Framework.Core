using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MegaZord.Framework.Compatible {
     public class DbEntityValidationException : System.Exception {
        
        public IList<ValidationResult> EntityValidationErrors { get; private set; }
    }
}
