using System.Data.Entity.Migrations;

namespace OnlineSatisProje.Data.Migrations
{
    public partial class IlkOlusturma : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Adres",
                    c => new
                    {
                        Id = c.Int(false, true),
                        Baslik = c.String(false, 150),
                        Adres = c.String(false),
                        IlceId = c.Int(false),
                        Telefon = c.String(maxLength: 15, fixedLength: true),
                        Aktif = c.Boolean(false),
                        CreatedDate = c.DateTime(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ilce", t => t.IlceId)
                .Index(t => t.IlceId);

            CreateTable(
                    "dbo.Ilce",
                    c => new
                    {
                        Id = c.Int(false, true),
                        SehirId = c.Int(false),
                        Ad = c.String(false, 30),
                        PostaKodu = c.String(maxLength: 5, fixedLength: true),
                        CreatedDate = c.DateTime(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sehir", t => t.SehirId)
                .Index(t => t.SehirId);

            CreateTable(
                    "dbo.Sehir",
                    c => new
                    {
                        Id = c.Int(false, true),
                        Ad = c.String(false, 30),
                        Plaka = c.String(false, 2, true),
                        CreatedDate = c.DateTime(false)
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.KullaniciAdresMapping",
                    c => new
                    {
                        Id = c.Int(false, true),
                        KullaniciId = c.String(false, 128),
                        AdresId = c.Int(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.KullaniciId)
                .ForeignKey("dbo.Adres", t => t.AdresId)
                .Index(t => t.KullaniciId)
                .Index(t => t.AdresId);

            CreateTable(
                    "dbo.AspNetUsers",
                    c => new
                    {
                        Id = c.String(false, 128),
                        Ad = c.String(),
                        Soyad = c.String(),
                        CreatedDate = c.DateTime(false),
                        UpdatedDate = c.DateTime(false),
                        IsDeleted = c.Boolean(false),
                        IsActive = c.Boolean(false),
                        ResimId = c.Int(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(false),
                        TwoFactorEnabled = c.Boolean(false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(false),
                        AccessFailedCount = c.Int(false),
                        UserName = c.String(false, 256)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                    "dbo.AspNetUserClaims",
                    c => new
                    {
                        Id = c.Int(false, true),
                        UserId = c.String(false, 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, true)
                .Index(t => t.UserId);

            CreateTable(
                    "dbo.AspNetUserLogins",
                    c => new
                    {
                        LoginProvider = c.String(false, 128),
                        ProviderKey = c.String(false, 128),
                        UserId = c.String(false, 128)
                    })
                .PrimaryKey(t => new {t.LoginProvider, t.ProviderKey, t.UserId})
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, true)
                .Index(t => t.UserId);

            CreateTable(
                    "dbo.AspNetUserRoles",
                    c => new
                    {
                        UserId = c.String(false, 128),
                        RoleId = c.String(false, 128)
                    })
                .PrimaryKey(t => new {t.UserId, t.RoleId})
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                    "dbo.SepetItem",
                    c => new
                    {
                        Id = c.Int(false, true),
                        KullaniciId = c.String(false, 128),
                        UrunId = c.Int(false),
                        Miktar = c.Int(false),
                        Aktif = c.Boolean(false),
                        CreatedDate = c.DateTime(false),
                        UpdatedDate = c.DateTime(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Urun", t => t.UrunId)
                .ForeignKey("dbo.AspNetUsers", t => t.KullaniciId)
                .Index(t => t.KullaniciId)
                .Index(t => t.UrunId);

            CreateTable(
                    "dbo.Urun",
                    c => new
                    {
                        Id = c.Int(false, true),
                        Baslik = c.String(false, 250),
                        KisaAciklama = c.String(false, 300),
                        TamAciklama = c.String(false),
                        AnasayfadaGoster = c.Boolean(false),
                        KullaniciYorumIzinVer = c.Boolean(false),
                        KargoAktif = c.Boolean(false),
                        UcretsizKargo = c.Boolean(false),
                        Fiyat = c.Decimal(false, 18, 4),
                        Aktif = c.Boolean(false),
                        Silindi = c.Boolean(false),
                        Yayinlandi = c.Boolean(false),
                        CreatedDate = c.DateTime(false),
                        UpdatedDate = c.DateTime(false),
                        SaticiId = c.Int(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Satici", t => t.SaticiId, true)
                .Index(t => t.SaticiId);

            CreateTable(
                    "dbo.Satici",
                    c => new
                    {
                        Id = c.Int(false, true),
                        KullaniciId = c.String(maxLength: 128),
                        Ad = c.String(maxLength: 150),
                        Aciklama = c.String(maxLength: 1000),
                        Email = c.String(maxLength: 100),
                        LogoId = c.Int(),
                        Aktif = c.Boolean(false),
                        Silindi = c.Boolean(false),
                        CreatedDate = c.DateTime(false),
                        UpdatedDate = c.DateTime(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.KullaniciId)
                .Index(t => t.KullaniciId);

            CreateTable(
                    "dbo.SaticiEtiketMapping",
                    c => new
                    {
                        Id = c.Int(false, true),
                        SaticiId = c.Int(false),
                        EtiketId = c.Int(false),
                        CreatedDate = c.DateTime(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Etiket", t => t.EtiketId)
                .ForeignKey("dbo.Satici", t => t.SaticiId)
                .Index(t => t.SaticiId)
                .Index(t => t.EtiketId);

            CreateTable(
                    "dbo.Etiket",
                    c => new
                    {
                        Id = c.Int(false, true),
                        Ad = c.String(false, 50),
                        CreatedDate = c.DateTime(false)
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.UrunEtiketMapping",
                    c => new
                    {
                        Id = c.Int(false, true),
                        UrunId = c.Int(false),
                        EtiketId = c.Int(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Urun", t => t.UrunId, true)
                .ForeignKey("dbo.Etiket", t => t.EtiketId)
                .Index(t => t.UrunId)
                .Index(t => t.EtiketId);

            CreateTable(
                    "dbo.SaticiIndirimMapping",
                    c => new
                    {
                        Id = c.Int(false, true),
                        SaticiId = c.Int(false),
                        IndirimId = c.Int(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Indirim", t => t.IndirimId)
                .ForeignKey("dbo.Satici", t => t.SaticiId)
                .Index(t => t.SaticiId)
                .Index(t => t.IndirimId);

            CreateTable(
                    "dbo.Indirim",
                    c => new
                    {
                        Id = c.Int(false, true),
                        Baslik = c.String(maxLength: 50),
                        YuzdeKullan = c.Boolean(false),
                        IndirimYuzdesi = c.Int(false),
                        IndirimMiktari = c.Int(false),
                        BaslangicTarihi = c.DateTime(false),
                        BitisTarihi = c.DateTime(false),
                        Aktif = c.Boolean(false)
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.UrunIndirimMapping",
                    c => new
                    {
                        Id = c.Int(false, true),
                        UrunId = c.Int(false),
                        IndirimId = c.Int(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Indirim", t => t.IndirimId)
                .ForeignKey("dbo.Urun", t => t.UrunId)
                .Index(t => t.UrunId)
                .Index(t => t.IndirimId);

            CreateTable(
                    "dbo.SaticiMekanMapping",
                    c => new
                    {
                        Id = c.Int(false, true),
                        SaticiId = c.Int(false),
                        AdresId = c.Int(false),
                        Aciklama = c.String(maxLength: 500),
                        Aktif = c.Boolean(false),
                        Silindi = c.Boolean(false),
                        CreatedDate = c.DateTime(false),
                        UpdatedDate = c.DateTime(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Satici", t => t.SaticiId)
                .ForeignKey("dbo.Adres", t => t.AdresId)
                .Index(t => t.SaticiId)
                .Index(t => t.AdresId);

            CreateTable(
                    "dbo.SiparisItem",
                    c => new
                    {
                        Id = c.Int(false, true),
                        SiparisItemGuid = c.Guid(false),
                        SiparisId = c.Int(false),
                        UrunId = c.Int(false),
                        Miktar = c.Int(false),
                        SaticiId = c.Int(false),
                        IndirimMiktari = c.Decimal(false, 18, 4),
                        Fiyat = c.Decimal(false, 18, 4)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Satici", t => t.SaticiId, true)
                .ForeignKey("dbo.Siparis", t => t.SiparisId)
                .ForeignKey("dbo.Urun", t => t.UrunId)
                .Index(t => t.SiparisId)
                .Index(t => t.UrunId)
                .Index(t => t.SaticiId);

            CreateTable(
                    "dbo.Siparis",
                    c => new
                    {
                        Id = c.Int(false, true),
                        SiparisGuid = c.Guid(false),
                        KullaniciId = c.String(false, 128),
                        KargoAdresId = c.Int(false),
                        SiparisDurumId = c.Int(),
                        OdemeDurumId = c.Int(),
                        KargoDurumId = c.Int(),
                        SiparisToplam = c.Decimal(false, 18, 0),
                        KartAdi = c.String(),
                        KartNumarasi = c.String(),
                        KartCvv = c.String(),
                        KartSonKullanimAy = c.String(),
                        KartSonKullanimYil = c.String(),
                        Aktif = c.Boolean(false),
                        Silindi = c.Boolean(false),
                        Tarih = c.DateTime(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.KullaniciId)
                .ForeignKey("dbo.Adres", t => t.KargoAdresId)
                .Index(t => t.KullaniciId)
                .Index(t => t.KargoAdresId);

            CreateTable(
                    "dbo.UrunKategoriMapping",
                    c => new
                    {
                        Id = c.Int(false, true),
                        UrunId = c.Int(false),
                        KategoriId = c.Int(false),
                        CreatedDate = c.DateTime(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kategori", t => t.KategoriId)
                .ForeignKey("dbo.Urun", t => t.UrunId)
                .Index(t => t.UrunId)
                .Index(t => t.KategoriId);

            CreateTable(
                    "dbo.Kategori",
                    c => new
                    {
                        Id = c.Int(false, true),
                        Ad = c.String(false, 150),
                        Aciklama = c.String(),
                        AnaKategoriId = c.Int(),
                        ResimId = c.Int(),
                        Aktif = c.Boolean(false),
                        CreatedDate = c.DateTime(false),
                        UpdatedDate = c.DateTime()
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.UrunResimMapping",
                    c => new
                    {
                        Id = c.Int(false, true),
                        UrunId = c.Int(false),
                        ResimId = c.Int(false)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Resim", t => t.ResimId)
                .ForeignKey("dbo.Urun", t => t.UrunId)
                .Index(t => t.UrunId)
                .Index(t => t.ResimId);

            CreateTable(
                    "dbo.Resim",
                    c => new
                    {
                        Id = c.Int(false, true),
                        ResimBinary = c.Binary(),
                        ResimPath = c.String(),
                        Baslik = c.String(),
                        AltAttr = c.String(),
                        TitleAttr = c.String(),
                        CreatedDate = c.DateTime(false)
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.AspNetRoles",
                    c => new
                    {
                        Id = c.String(false, 128),
                        Name = c.String(false, 256)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
        }

        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Siparis", "KargoAdresId", "dbo.Adres");
            DropForeignKey("dbo.SaticiMekanMapping", "AdresId", "dbo.Adres");
            DropForeignKey("dbo.KullaniciAdresMapping", "AdresId", "dbo.Adres");
            DropForeignKey("dbo.Siparis", "KullaniciId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SepetItem", "KullaniciId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UrunResimMapping", "UrunId", "dbo.Urun");
            DropForeignKey("dbo.UrunResimMapping", "ResimId", "dbo.Resim");
            DropForeignKey("dbo.UrunKategoriMapping", "UrunId", "dbo.Urun");
            DropForeignKey("dbo.UrunKategoriMapping", "KategoriId", "dbo.Kategori");
            DropForeignKey("dbo.UrunIndirimMapping", "UrunId", "dbo.Urun");
            DropForeignKey("dbo.SiparisItem", "UrunId", "dbo.Urun");
            DropForeignKey("dbo.SepetItem", "UrunId", "dbo.Urun");
            DropForeignKey("dbo.Urun", "SaticiId", "dbo.Satici");
            DropForeignKey("dbo.SiparisItem", "SiparisId", "dbo.Siparis");
            DropForeignKey("dbo.SiparisItem", "SaticiId", "dbo.Satici");
            DropForeignKey("dbo.SaticiMekanMapping", "SaticiId", "dbo.Satici");
            DropForeignKey("dbo.SaticiIndirimMapping", "SaticiId", "dbo.Satici");
            DropForeignKey("dbo.UrunIndirimMapping", "IndirimId", "dbo.Indirim");
            DropForeignKey("dbo.SaticiIndirimMapping", "IndirimId", "dbo.Indirim");
            DropForeignKey("dbo.SaticiEtiketMapping", "SaticiId", "dbo.Satici");
            DropForeignKey("dbo.UrunEtiketMapping", "EtiketId", "dbo.Etiket");
            DropForeignKey("dbo.UrunEtiketMapping", "UrunId", "dbo.Urun");
            DropForeignKey("dbo.SaticiEtiketMapping", "EtiketId", "dbo.Etiket");
            DropForeignKey("dbo.Satici", "KullaniciId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.KullaniciAdresMapping", "KullaniciId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ilce", "SehirId", "dbo.Sehir");
            DropForeignKey("dbo.Adres", "IlceId", "dbo.Ilce");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.UrunResimMapping", new[] {"ResimId"});
            DropIndex("dbo.UrunResimMapping", new[] {"UrunId"});
            DropIndex("dbo.UrunKategoriMapping", new[] {"KategoriId"});
            DropIndex("dbo.UrunKategoriMapping", new[] {"UrunId"});
            DropIndex("dbo.Siparis", new[] {"KargoAdresId"});
            DropIndex("dbo.Siparis", new[] {"KullaniciId"});
            DropIndex("dbo.SiparisItem", new[] {"SaticiId"});
            DropIndex("dbo.SiparisItem", new[] {"UrunId"});
            DropIndex("dbo.SiparisItem", new[] {"SiparisId"});
            DropIndex("dbo.SaticiMekanMapping", new[] {"AdresId"});
            DropIndex("dbo.SaticiMekanMapping", new[] {"SaticiId"});
            DropIndex("dbo.UrunIndirimMapping", new[] {"IndirimId"});
            DropIndex("dbo.UrunIndirimMapping", new[] {"UrunId"});
            DropIndex("dbo.SaticiIndirimMapping", new[] {"IndirimId"});
            DropIndex("dbo.SaticiIndirimMapping", new[] {"SaticiId"});
            DropIndex("dbo.UrunEtiketMapping", new[] {"EtiketId"});
            DropIndex("dbo.UrunEtiketMapping", new[] {"UrunId"});
            DropIndex("dbo.SaticiEtiketMapping", new[] {"EtiketId"});
            DropIndex("dbo.SaticiEtiketMapping", new[] {"SaticiId"});
            DropIndex("dbo.Satici", new[] {"KullaniciId"});
            DropIndex("dbo.Urun", new[] {"SaticiId"});
            DropIndex("dbo.SepetItem", new[] {"UrunId"});
            DropIndex("dbo.SepetItem", new[] {"KullaniciId"});
            DropIndex("dbo.AspNetUserRoles", new[] {"RoleId"});
            DropIndex("dbo.AspNetUserRoles", new[] {"UserId"});
            DropIndex("dbo.AspNetUserLogins", new[] {"UserId"});
            DropIndex("dbo.AspNetUserClaims", new[] {"UserId"});
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.KullaniciAdresMapping", new[] {"AdresId"});
            DropIndex("dbo.KullaniciAdresMapping", new[] {"KullaniciId"});
            DropIndex("dbo.Ilce", new[] {"SehirId"});
            DropIndex("dbo.Adres", new[] {"IlceId"});
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Resim");
            DropTable("dbo.UrunResimMapping");
            DropTable("dbo.Kategori");
            DropTable("dbo.UrunKategoriMapping");
            DropTable("dbo.Siparis");
            DropTable("dbo.SiparisItem");
            DropTable("dbo.SaticiMekanMapping");
            DropTable("dbo.UrunIndirimMapping");
            DropTable("dbo.Indirim");
            DropTable("dbo.SaticiIndirimMapping");
            DropTable("dbo.UrunEtiketMapping");
            DropTable("dbo.Etiket");
            DropTable("dbo.SaticiEtiketMapping");
            DropTable("dbo.Satici");
            DropTable("dbo.Urun");
            DropTable("dbo.SepetItem");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.KullaniciAdresMapping");
            DropTable("dbo.Sehir");
            DropTable("dbo.Ilce");
            DropTable("dbo.Adres");
        }
    }
}