using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CoinsCollection.Entities
{
    public class Coin
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Nomination { get; set; } = string.Empty;
        public string? Synonyms { get; set; }

        public string? Description { get; set; }
        
        public int? Year { get; set; }

        public byte[]? Avers { get; set; }

        public byte[]? Revers { get; set; }

        public decimal? Weight { get; set; }
        public decimal? Diameter { get; set; }
        public decimal? Width { get; set; }

        public int? AlbumPage { get; set; }
        public int? AlbumRow { get; set; }
        public int? AlbumColumn { get; set; }
        public int? AlbumID { get; set; }
        public int? CountryID { get; set; }
        public int? MaterialID { get; set; }
        
        public virtual CoinAlbum? Album { get; set; }
        public virtual CoinCountry? Country { get; set; }
        public virtual CoinMaterial? Material { get; set; }
    }
}
