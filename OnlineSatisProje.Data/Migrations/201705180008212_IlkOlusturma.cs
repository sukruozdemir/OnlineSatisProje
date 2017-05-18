namespace OnlineSatisProje.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IlkOlusturma : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Baslik = c.String(nullable: false, maxLength: 150),
                        Adres = c.String(nullable: false),
                        IlceId = c.Int(nullable: false),
                        Telefon = c.String(maxLength: 15, fixedLength: true),
                        Aktif = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ilce", t => t.IlceId)
                .Index(t => t.IlceId);
            
            CreateTable(
                "dbo.Ilce",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SehirId = c.Int(nullable: false),
                        Ad = c.String(nullable: false, maxLength: 30),
                        PostaKodu = c.String(maxLength: 5, fixedLength: true),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sehir", t => t.SehirId)
                .Index(t => t.SehirId);
            
            CreateTable(
                "dbo.Sehir",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(nullable: false, maxLength: 30),
                        Plaka = c.String(nullable: false, maxLength: 2, fixedLength: true),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.KullaniciAdresMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KullaniciId = c.String(nullable: false, maxLength: 128),
                        AdresId = c.Int(nullable: false),
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
                        Id = c.String(nullable: false, maxLength: 128),
                        Ad = c.String(),
                        Soyad = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ResimId = c.Int(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SepetItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KullaniciId = c.String(nullable: false, maxLength: 128),
                        UrunId = c.Int(nullable: false),
                        Miktar = c.Int(nullable: false),
                        Aktif = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
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
                        Id = c.Int(nullable: false, identity: true),
                        Baslik = c.String(nullable: false, maxLength: 250),
                        KisaAciklama = c.String(nullable: false, maxLength: 300),
                        TamAciklama = c.String(nullable: false),
                        AnasayfadaGoster = c.Boolean(nullable: false),
                        KullaniciYorumIzinVer = c.Boolean(nullable: false),
                        KargoAktif = c.Boolean(nullable: false),
                        UcretsizKargo = c.Boolean(nullable: false),
                        Fiyat = c.Decimal(nullable: false, precision: 18, scale: 4),
                        Aktif = c.Boolean(nullable: false),
                        Silindi = c.Boolean(nullable: false),
                        Yayinlandi = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        SaticiId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Satici", t => t.SaticiId, cascadeDelete: true)
                .Index(t => t.SaticiId);
            
            CreateTable(
                "dbo.Satici",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KullaniciId = c.String(maxLength: 128),
                        Ad = c.String(maxLength: 150),
                        Aciklama = c.String(maxLength: 1000),
                        Email = c.String(maxLength: 100),
                        LogoId = c.Int(),
                        Aktif = c.Boolean(nullable: false),
                        Silindi = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.KullaniciId)
                .Index(t => t.KullaniciId);
            
            CreateTable(
                "dbo.SaticiEtiketMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SaticiId = c.Int(nullable: false),
                        EtiketId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
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
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(nullable: false, maxLength: 50),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UrunEtiketMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrunId = c.Int(nullable: false),
                        EtiketId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Urun", t => t.UrunId, cascadeDelete: true)
                .ForeignKey("dbo.Etiket", t => t.EtiketId)
                .Index(t => t.UrunId)
                .Index(t => t.EtiketId);
            
            CreateTable(
                "dbo.SaticiIndirimMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SaticiId = c.Int(nullable: false),
                        IndirimId = c.Int(nullable: false),
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
                        Id = c.Int(nullable: false, identity: true),
                        Baslik = c.String(maxLength: 50),
                        YuzdeKullan = c.Boolean(nullable: false),
                        IndirimYuzdesi = c.Int(nullable: false),
                        IndirimMiktari = c.Int(nullable: false),
                        BaslangicTarihi = c.DateTime(nullable: false),
                        BitisTarihi = c.DateTime(nullable: false),
                        Aktif = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UrunIndirimMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrunId = c.Int(nullable: false),
                        IndirimId = c.Int(nullable: false),
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
                        Id = c.Int(nullable: false, identity: true),
                        SaticiId = c.Int(nullable: false),
                        AdresId = c.Int(nullable: false),
                        Aciklama = c.String(maxLength: 500),
                        Aktif = c.Boolean(nullable: false),
                        Silindi = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
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
                        Id = c.Int(nullable: false, identity: true),
                        SiparisItemGuid = c.Guid(nullable: false),
                        SiparisId = c.Int(nullable: false),
                        UrunId = c.Int(nullable: false),
                        Miktar = c.Int(nullable: false),
                        SaticiId = c.Int(nullable: false),
                        IndirimMiktari = c.Decimal(nullable: false, precision: 18, scale: 4),
                        Fiyat = c.Decimal(nullable: false, precision: 18, scale: 4),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Satici", t => t.SaticiId, cascadeDelete: true)
                .ForeignKey("dbo.Siparis", t => t.SiparisId)
                .ForeignKey("dbo.Urun", t => t.UrunId)
                .Index(t => t.SiparisId)
                .Index(t => t.UrunId)
                .Index(t => t.SaticiId);
            
            CreateTable(
                "dbo.Siparis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiparisGuid = c.Guid(nullable: false),
                        KullaniciId = c.String(nullable: false, maxLength: 128),
                        KargoAdresId = c.Int(nullable: false),
                        SiparisDurumId = c.Int(),
                        OdemeDurumId = c.Int(),
                        KargoDurumId = c.Int(),
                        SiparisToplam = c.Decimal(nullable: false, precision: 18, scale: 0),
                        KartAdi = c.String(),
                        KartNumarasi = c.String(),
                        KartCvv = c.String(),
                        KartSonKullanimAy = c.String(),
                        KartSonKullanimYil = c.String(),
                        Aktif = c.Boolean(nullable: false),
                        Silindi = c.Boolean(nullable: false),
                        Tarih = c.DateTime(nullable: false),
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
                        Id = c.Int(nullable: false, identity: true),
                        UrunId = c.Int(nullable: false),
                        KategoriId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
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
                        Id = c.Int(nullable: false, identity: true),
                        Ad = c.String(nullable: false, maxLength: 150),
                        Aciklama = c.String(),
                        AnaKategoriId = c.Int(),
                        ResimId = c.Int(),
                        Aktif = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UrunResimMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrunId = c.Int(nullable: false),
                        ResimId = c.Int(nullable: false),
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
                        Id = c.Int(nullable: false, identity: true),
                        ResimBinary = c.Binary(),
                        ResimPath = c.String(),
                        Baslik = c.String(),
                        AltAttr = c.String(),
                        TitleAttr = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
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
            DropIndex("dbo.UrunResimMapping", new[] { "ResimId" });
            DropIndex("dbo.UrunResimMapping", new[] { "UrunId" });
            DropIndex("dbo.UrunKategoriMapping", new[] { "KategoriId" });
            DropIndex("dbo.UrunKategoriMapping", new[] { "UrunId" });
            DropIndex("dbo.Siparis", new[] { "KargoAdresId" });
            DropIndex("dbo.Siparis", new[] { "KullaniciId" });
            DropIndex("dbo.SiparisItem", new[] { "SaticiId" });
            DropIndex("dbo.SiparisItem", new[] { "UrunId" });
            DropIndex("dbo.SiparisItem", new[] { "SiparisId" });
            DropIndex("dbo.SaticiMekanMapping", new[] { "AdresId" });
            DropIndex("dbo.SaticiMekanMapping", new[] { "SaticiId" });
            DropIndex("dbo.UrunIndirimMapping", new[] { "IndirimId" });
            DropIndex("dbo.UrunIndirimMapping", new[] { "UrunId" });
            DropIndex("dbo.SaticiIndirimMapping", new[] { "IndirimId" });
            DropIndex("dbo.SaticiIndirimMapping", new[] { "SaticiId" });
            DropIndex("dbo.UrunEtiketMapping", new[] { "EtiketId" });
            DropIndex("dbo.UrunEtiketMapping", new[] { "UrunId" });
            DropIndex("dbo.SaticiEtiketMapping", new[] { "EtiketId" });
            DropIndex("dbo.SaticiEtiketMapping", new[] { "SaticiId" });
            DropIndex("dbo.Satici", new[] { "KullaniciId" });
            DropIndex("dbo.Urun", new[] { "SaticiId" });
            DropIndex("dbo.SepetItem", new[] { "UrunId" });
            DropIndex("dbo.SepetItem", new[] { "KullaniciId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.KullaniciAdresMapping", new[] { "AdresId" });
            DropIndex("dbo.KullaniciAdresMapping", new[] { "KullaniciId" });
            DropIndex("dbo.Ilce", new[] { "SehirId" });
            DropIndex("dbo.Adres", new[] { "IlceId" });
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
