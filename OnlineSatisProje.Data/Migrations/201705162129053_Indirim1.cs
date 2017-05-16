namespace OnlineSatisProje.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Indirim1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Indirim", "IndirimYuzdesi", c => c.Int(nullable: false));
            AlterColumn("dbo.Indirim", "IndirimMiktari", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Indirim", "IndirimMiktari", c => c.Decimal(nullable: false, precision: 18, scale: 4));
            AlterColumn("dbo.Indirim", "IndirimYuzdesi", c => c.Decimal(nullable: false, precision: 18, scale: 4));
        }
    }
}
