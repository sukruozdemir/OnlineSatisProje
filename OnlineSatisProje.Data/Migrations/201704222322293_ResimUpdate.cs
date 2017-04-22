namespace OnlineSatisProje.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResimUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Resim", "ResimPath", c => c.String());
            AlterColumn("dbo.Resim", "ResimBinary", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Resim", "ResimBinary", c => c.Binary(nullable: true));
            DropColumn("dbo.Resim", "ResimPath");
        }
    }
}
