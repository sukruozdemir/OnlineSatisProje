namespace OnlineSatisProje.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KullaniciUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Ad", c => c.String());
            AddColumn("dbo.AspNetUsers", "Soyad", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Soyad");
            DropColumn("dbo.AspNetUsers", "Ad");
        }
    }
}
