using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using OnlineSatisProje.Core.Entities;

namespace OnlineSatisProje.Data
{
    //IdentityDbContext<Kullanici>
    public class ApplicationContext : IdentityDbContext<Kullanici>, IDbContext
    {
        public ApplicationContext(string connectionStringName = "LocalConnection") : base(connectionStringName)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationContext>());
        }

        public virtual DbSet<Adres> Adres { get; set; }
        public virtual DbSet<Etiket> Etiket { get; set; }
        public virtual DbSet<Ilce> Ilce { get; set; }
        public virtual DbSet<Indirim> Indirim { get; set; }
        public virtual DbSet<Kategori> Kategori { get; set; }
        public virtual DbSet<KullaniciAdresMapping> KullaniciAdresMapping { get; set; }
        public virtual DbSet<Resim> Resim { get; set; }
        public virtual DbSet<Satici> Satici { get; set; }
        public virtual DbSet<SaticiEtiketMapping> SaticiEtiketMapping { get; set; }
        public virtual DbSet<SaticiIndirimMapping> SaticiIndirimMapping { get; set; }
        public virtual DbSet<SaticiMekanMapping> SaticiMekanMapping { get; set; }
        public virtual DbSet<Sehir> Sehir { get; set; }
        public virtual DbSet<SepetItem> SepetItem { get; set; }
        public virtual DbSet<Siparis> Siparis { get; set; }
        public virtual DbSet<SiparisItem> SiparisItem { get; set; }
        public virtual DbSet<Urun> Urun { get; set; }
        public virtual DbSet<UrunEtiketMapping> UrunEtiketMapping { get; set; }
        public virtual DbSet<UrunIndirimMapping> UrunIndirimMapping { get; set; }
        public virtual DbSet<UrunKategoriMapping> UrunKategoriMapping { get; set; }
        public virtual DbSet<UrunResimMapping> UrunResimMapping { get; set; }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Adres>().Property(a => a.Adres1).IsMaxLength();
            modelBuilder.Entity<Adres>()
                .Property(e => e.Telefon)
                .IsFixedLength();

            modelBuilder.Entity<Adres>()
                .HasMany(e => e.KullaniciAdresMapping)
                .WithRequired(e => e.Adres)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Adres>()
                .HasMany(e => e.SaticiMekanMapping)
                .WithRequired(e => e.Adres)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Adres>()
                .HasMany(e => e.Siparis)
                .WithRequired(e => e.Adres)
                .HasForeignKey(e => e.KargoAdresId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Etiket>()
                .HasMany(e => e.SaticiEtiketMapping)
                .WithRequired(e => e.Etiket)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Etiket>()
                .HasMany(e => e.UrunEtiketMapping)
                .WithRequired(e => e.Etiket)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ilce>()
                .Property(e => e.PostaKodu)
                .IsFixedLength();

            modelBuilder.Entity<Ilce>()
                .HasMany(e => e.Adres)
                .WithRequired(e => e.Ilce)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Indirim>()
                .HasMany(e => e.SaticiIndirimMapping)
                .WithRequired(e => e.Indirim)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Indirim>()
                .HasMany(e => e.UrunIndirimMapping)
                .WithRequired(e => e.Indirim)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kategori>()
                .HasMany(e => e.UrunKategoriMapping)
                .WithRequired(e => e.Kategori)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.KullaniciAdresMapping)
                .WithRequired(e => e.Kullanici)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.Siparis)
                .WithRequired(e => e.Kullanici)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.SepetItem)
                .WithRequired(e => e.Kullanici)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Resim>().Property(r => r.Baslik).IsMaxLength();
            modelBuilder.Entity<Resim>().Property(r => r.AltAttr).IsMaxLength();
            modelBuilder.Entity<Resim>().Property(r => r.TitleAttr).IsMaxLength();


            modelBuilder.Entity<Resim>()
                .HasMany(e => e.UrunResimMapping)
                .WithRequired(e => e.Resim)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Satici>()
                .HasMany(e => e.SaticiEtiketMapping)
                .WithRequired(e => e.Satici)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Satici>()
                .HasMany(e => e.SaticiIndirimMapping)
                .WithRequired(e => e.Satici)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Satici>()
                .HasMany(e => e.SaticiMekanMapping)
                .WithRequired(e => e.Satici)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sehir>()
                .Property(e => e.Plaka)
                .IsFixedLength();

            modelBuilder.Entity<Sehir>()
                .HasMany(e => e.Ilce)
                .WithRequired(e => e.Sehir)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Siparis>()
                .Property(e => e.SiparisToplam)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Siparis>()
                .HasMany(e => e.SiparisItem)
                .WithRequired(e => e.Siparis)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SiparisItem>()
                .Property(e => e.IndirimMiktari)
                .HasPrecision(18, 4);

            modelBuilder.Entity<SiparisItem>()
                .Property(e => e.Fiyat)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Urun>()
                .Property(e => e.Fiyat)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Urun>()
                .HasMany(e => e.SepetItem)
                .WithRequired(e => e.Urun)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Urun>()
                .HasMany(e => e.SiparisItem)
                .WithRequired(e => e.Urun)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Urun>()
                .HasMany(e => e.UrunIndirimMapping)
                .WithRequired(e => e.Urun)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Urun>()
                .HasMany(e => e.UrunKategoriMapping)
                .WithRequired(e => e.Urun)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Urun>()
                .HasMany(e => e.UrunResimMapping)
                .WithRequired(e => e.Urun)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}