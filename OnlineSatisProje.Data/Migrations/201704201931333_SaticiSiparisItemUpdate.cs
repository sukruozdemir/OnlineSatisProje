namespace OnlineSatisProje.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaticiSiparisItemUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SiparisItem", "SaticiId", c => c.Int(nullable: true));
            CreateIndex("dbo.SiparisItem", "SaticiId");
            AddForeignKey("dbo.SiparisItem", "SaticiId", "dbo.Satici", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SiparisItem", "SaticiId", "dbo.Satici");
            DropIndex("dbo.SiparisItem", new[] { "SaticiId" });
            DropColumn("dbo.SiparisItem", "SaticiId");
        }
    }
}
