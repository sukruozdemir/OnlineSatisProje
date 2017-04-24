namespace OnlineSatisProje.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KullaniciUpdate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ResimId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ResimId");
        }
    }
}
