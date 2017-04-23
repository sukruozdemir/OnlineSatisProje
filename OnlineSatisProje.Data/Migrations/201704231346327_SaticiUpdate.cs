namespace OnlineSatisProje.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaticiUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Satici", "Aktif", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Satici", "Silindi", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Satici", "Silindi", c => c.Boolean());
            AlterColumn("dbo.Satici", "Aktif", c => c.Boolean());
        }
    }
}
