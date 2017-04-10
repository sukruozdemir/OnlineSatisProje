namespace OnlineSatisProje.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dbupdate : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.KullaniciAdresMapping", new[] { "Kullanici_Id" });
            DropIndex("dbo.SepetItem", new[] { "Kullanici_Id" });
            DropIndex("dbo.Siparis", new[] { "Kullanici_Id" });
            DropColumn("dbo.KullaniciAdresMapping", "KullaniciId");
            DropColumn("dbo.SepetItem", "KullaniciId");
            DropColumn("dbo.Siparis", "KullaniciId");
            RenameColumn(table: "dbo.KullaniciAdresMapping", name: "Kullanici_Id", newName: "KullaniciId");
            RenameColumn(table: "dbo.SepetItem", name: "Kullanici_Id", newName: "KullaniciId");
            RenameColumn(table: "dbo.Siparis", name: "Kullanici_Id", newName: "KullaniciId");
            AddColumn("dbo.Satici", "KullaniciId", c => c.String(maxLength: 128));
            AlterColumn("dbo.KullaniciAdresMapping", "KullaniciId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.SepetItem", "KullaniciId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Siparis", "KullaniciId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.KullaniciAdresMapping", "KullaniciId");
            CreateIndex("dbo.SepetItem", "KullaniciId");
            CreateIndex("dbo.Satici", "KullaniciId");
            CreateIndex("dbo.Siparis", "KullaniciId");
            AddForeignKey("dbo.Satici", "KullaniciId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Satici", "KullaniciId", "dbo.AspNetUsers");
            DropIndex("dbo.Siparis", new[] { "KullaniciId" });
            DropIndex("dbo.Satici", new[] { "KullaniciId" });
            DropIndex("dbo.SepetItem", new[] { "KullaniciId" });
            DropIndex("dbo.KullaniciAdresMapping", new[] { "KullaniciId" });
            AlterColumn("dbo.Siparis", "KullaniciId", c => c.Int(nullable: false));
            AlterColumn("dbo.SepetItem", "KullaniciId", c => c.Int(nullable: false));
            AlterColumn("dbo.KullaniciAdresMapping", "KullaniciId", c => c.Int(nullable: false));
            DropColumn("dbo.Satici", "KullaniciId");
            RenameColumn(table: "dbo.Siparis", name: "KullaniciId", newName: "Kullanici_Id");
            RenameColumn(table: "dbo.SepetItem", name: "KullaniciId", newName: "Kullanici_Id");
            RenameColumn(table: "dbo.KullaniciAdresMapping", name: "KullaniciId", newName: "Kullanici_Id");
            AddColumn("dbo.Siparis", "KullaniciId", c => c.Int(nullable: false));
            AddColumn("dbo.SepetItem", "KullaniciId", c => c.Int(nullable: false));
            AddColumn("dbo.KullaniciAdresMapping", "KullaniciId", c => c.Int(nullable: false));
            CreateIndex("dbo.Siparis", "Kullanici_Id");
            CreateIndex("dbo.SepetItem", "Kullanici_Id");
            CreateIndex("dbo.KullaniciAdresMapping", "Kullanici_Id");
        }
    }
}
