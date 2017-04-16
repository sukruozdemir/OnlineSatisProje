namespace OnlineSatisProje.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Satici_Adres_Update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Adres", "Aktif", c => c.Boolean(nullable: false));
            AddColumn("dbo.SaticiMekanMapping", "Aktif", c => c.Boolean(nullable: false));
            AddColumn("dbo.SaticiMekanMapping", "Silindi", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SaticiMekanMapping", "Silindi");
            DropColumn("dbo.SaticiMekanMapping", "Aktif");
            DropColumn("dbo.Adres", "Aktif");
        }
    }
}
