using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CoinsCollection.Entities
{
    public class CoinMaterial
    {
        public int ID {  get; set; }   
        public string Name { get; set; } = string.Empty;
        public string? Synonyms { get; set; }        
        public virtual ICollection<Coin>? Coins { get; set; }
        public int CoinsCount
        {
            get
            {
                return Coins == null ? 0 : Coins.Count;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (Name.IsNullOrEmpty())
            {
                errors.Add(new ValidationResult("Material name is empty"));
            }
            return errors;
        }
    }
}
