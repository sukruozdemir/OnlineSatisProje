namespace OnlineSatisProje.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Saticitableupdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Satici", "LogoId", "dbo.Resim");
            DropIndex("dbo.Satici", new[] { "LogoId" });
            AlterColumn("dbo.Satici", "Ad", c => c.String(maxLength: 150));
            AlterColumn("dbo.Satici", "LogoId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Satici", "LogoId", c => c.Int(nullable: false));
            AlterColumn("dbo.Satici", "Ad", c => c.String(nullable: false, maxLength: 150));
            CreateIndex("dbo.Satici", "LogoId");
            AddForeignKey("dbo.Satici", "LogoId", "dbo.Resim", "Id");
        }
    }
}
