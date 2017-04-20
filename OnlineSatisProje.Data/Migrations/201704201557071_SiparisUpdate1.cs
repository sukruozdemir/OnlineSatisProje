namespace OnlineSatisProje.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SiparisUpdate1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Siparis", "SiparisDurumu");
            DropColumn("dbo.Siparis", "OdemeDurumu");
            DropColumn("dbo.Siparis", "KargoDurumu");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Siparis", "KargoDurumu", c => c.Int(nullable: false));
            AddColumn("dbo.Siparis", "OdemeDurumu", c => c.Int(nullable: false));
            AddColumn("dbo.Siparis", "SiparisDurumu", c => c.Int(nullable: false));
        }
    }
}
