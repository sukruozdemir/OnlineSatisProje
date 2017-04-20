namespace OnlineSatisProje.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SiparisUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Siparis", "KartAdi", c => c.String());
            AddColumn("dbo.Siparis", "KartNumarasi", c => c.String());
            AddColumn("dbo.Siparis", "KartCvv", c => c.String());
            AddColumn("dbo.Siparis", "KartSonKullanimAy", c => c.String());
            AddColumn("dbo.Siparis", "KartSonKullanimYil", c => c.String());
            AddColumn("dbo.Siparis", "Tarih", c => c.DateTime(nullable: false));
            AddColumn("dbo.Siparis", "SiparisDurumu", c => c.Int(nullable: false));
            AddColumn("dbo.Siparis", "OdemeDurumu", c => c.Int(nullable: false));
            AddColumn("dbo.Siparis", "KargoDurumu", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Siparis", "KargoDurumu");
            DropColumn("dbo.Siparis", "OdemeDurumu");
            DropColumn("dbo.Siparis", "SiparisDurumu");
            DropColumn("dbo.Siparis", "Tarih");
            DropColumn("dbo.Siparis", "KartSonKullanimYil");
            DropColumn("dbo.Siparis", "KartSonKullanimAy");
            DropColumn("dbo.Siparis", "KartCvv");
            DropColumn("dbo.Siparis", "KartNumarasi");
            DropColumn("dbo.Siparis", "KartAdi");
        }
    }
}
