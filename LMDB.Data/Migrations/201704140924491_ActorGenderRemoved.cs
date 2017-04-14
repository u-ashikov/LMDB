namespace LMDB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActorGenderRemoved : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contributors", "Biography", c => c.String());
            DropColumn("dbo.Contributors", "Gender");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contributors", "Gender", c => c.Int(nullable: false));
            AlterColumn("dbo.Contributors", "Biography", c => c.String(nullable: false));
        }
    }
}
