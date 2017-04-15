namespace LMDB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMoviePosterField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Poster", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "Poster");
        }
    }
}
