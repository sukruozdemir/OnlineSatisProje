namespace OnlineSatisProje.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SepetItemUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SepetItem", "Aktif", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SepetItem", "Aktif");
        }
    }
}
