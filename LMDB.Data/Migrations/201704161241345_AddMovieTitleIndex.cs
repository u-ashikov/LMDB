namespace LMDB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMovieTitleIndex : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Movies", "Title", unique: true, name: "MovieTitleIndex");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Movies", "MovieTitleIndex");
        }
    }
}
