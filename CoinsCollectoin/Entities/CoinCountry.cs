using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinsCollection.Entities
{
    public class CoinCountry
    {
        
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;

        public int? StartYear { get; set; }

        public int? EndYear { get; set; }

        public virtual ICollection<Coin>? Coins { get; set; }

        public int CoinsCount
        {
            get
            {
                return Coins == null ? 0 : Coins.Count;
            }
        }

        public int From
        {
            get
            {
                return StartYear == null ? YearConstants.MinimumYear : (int)StartYear;
            }
        }

        public int Till
        {
            get
            {
                return EndYear == null ? YearConstants.MaximumYear : (int)EndYear;
            }
        }
    }
}
