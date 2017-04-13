namespace LMDB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initg : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contributors", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Contributors", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Contributors", "Biography", c => c.String(nullable: false));
            AlterColumn("dbo.Countries", "Name", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.AspNetUsers", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Password", c => c.String());
            AlterColumn("dbo.Countries", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Contributors", "Biography", c => c.String(nullable: false, maxLength: 2500));
            AlterColumn("dbo.Contributors", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Contributors", "FirstName", c => c.String(nullable: false));
        }
    }
}
