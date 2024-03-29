using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CoinsCollection.Entities
{
    public class CoinAlbum
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[]? Cover {  get; set; }

        public int ColumnsCount { get; set; }

        public int RowsCount { get; set; }

        public int PagesCount { get; set; }

        public virtual ICollection<Coin>? Coins { get; set; }
        public int CoinsCount
        {
            get
            {
                return Coins == null ? 0 : Coins.Count;
            }
        }

    }
}
