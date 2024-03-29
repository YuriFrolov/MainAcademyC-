using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CoinsCollection.Entities;

namespace CoinsCollection
{
    public class AppContext : DbContext
    {
        public DbSet<Coin> Coins { get; set; }
        public DbSet<CoinCountry> Countries { get; set; }
        public DbSet<CoinAlbum> Albums { get; set; }
        public DbSet<CoinMaterial> Materials { get; set; }

        public AppContext() : base()
        {
          //  Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Coins;Integrated Security=True");
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-RB59NBE\SQLEXPRESS;Database=Coins;Trusted_Connection=True;TrustServerCertificate=True;");
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoinMaterial>().HasKey(s => s.ID);
            modelBuilder.Entity<CoinMaterial>().Property(s => s.Name).IsRequired(true).IsUnicode().IsFixedLength(false).HasMaxLength(50);
            modelBuilder.Entity<CoinMaterial>().Property(s => s.Synonyms).IsRequired(false).IsUnicode().IsFixedLength(false).HasMaxLength(150);

            modelBuilder.Entity<CoinAlbum>().HasKey(s => s.ID);
            modelBuilder.Entity<CoinAlbum>().Property(s => s.Name).IsRequired(true).IsUnicode().IsFixedLength(false).HasMaxLength(50);            
            modelBuilder.Entity<CoinAlbum>().Property(s => s.PagesCount).IsRequired(true);
            modelBuilder.Entity<CoinAlbum>().Property(s => s.RowsCount).IsRequired(true);
            modelBuilder.Entity<CoinAlbum>().Property(s => s.ColumnsCount).IsRequired(true);
            modelBuilder.Entity<CoinAlbum>().Property(s => s.Cover).IsRequired(false).IsUnicode(false).IsFixedLength(false);
          
            modelBuilder.Entity<CoinCountry>().HasKey(s => s.ID);
            modelBuilder.Entity<CoinCountry>().Property(s => s.Name).IsRequired(true).IsUnicode().IsFixedLength(false).HasMaxLength(50);            
            modelBuilder.Entity<CoinCountry>().Property(s => s.StartYear).IsRequired(false);
            modelBuilder.Entity<CoinCountry>().Property(s => s.EndYear).IsRequired(false);

            modelBuilder.Entity<Coin>().HasKey(s => s.ID);
            modelBuilder.Entity<Coin>().Property(s => s.Name).IsRequired(true).IsUnicode().IsFixedLength(false).HasMaxLength(50);
            modelBuilder.Entity<Coin>().Property(s => s.Nomination).IsRequired(true).IsUnicode().IsFixedLength(false).HasMaxLength(10);
            modelBuilder.Entity<Coin>().Property(s => s.Synonyms).IsRequired(true).IsUnicode().IsFixedLength(false).HasMaxLength(150);
            modelBuilder.Entity<Coin>().Property(s => s.Description).IsRequired(true).IsUnicode().IsFixedLength(false).HasMaxLength(300);
            modelBuilder.Entity<Coin>().Property(s => s.Year).IsRequired(false);
            modelBuilder.Entity<Coin>().Property(s => s.Avers).IsRequired(false).IsUnicode(false).IsFixedLength(false);
            modelBuilder.Entity<Coin>().Property(s => s.Revers).IsRequired(false).IsUnicode(false).IsFixedLength(false);
            modelBuilder.Entity<Coin>().Property(s => s.Weight).IsRequired(false).HasPrecision(18, 2);
            modelBuilder.Entity<Coin>().Property(s => s.Diameter).IsRequired(false).HasPrecision(18, 2);
            modelBuilder.Entity<Coin>().Property(s => s.Width).IsRequired(false).HasPrecision(18, 2);
            modelBuilder.Entity<Coin>().HasOne(s => s.Material).WithMany(s => s.Coins).HasForeignKey(s => s.MaterialID).IsRequired(false);
            modelBuilder.Entity<Coin>().HasOne(s => s.Country).WithMany(s => s.Coins).HasForeignKey(s => s.CountryID).IsRequired(false);
            modelBuilder.Entity<Coin>().HasOne(s => s.Album).WithMany(s => s.Coins).HasForeignKey(s => s.AlbumID).IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
