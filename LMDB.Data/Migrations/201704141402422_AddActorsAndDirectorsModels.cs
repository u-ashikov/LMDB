namespace LMDB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActorsAndDirectorsModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Actors", "Id", "dbo.Contributors");
            DropForeignKey("dbo.Directors", "Id", "dbo.Contributors");
            DropForeignKey("dbo.ActorAwards", "ActorId", "dbo.Actors");
            DropForeignKey("dbo.MovieActors", "ActorId", "dbo.Actors");
            DropForeignKey("dbo.Movies", "DirectorId", "dbo.Directors");
            DropForeignKey("dbo.DirectorAwards", "ADirectorId", "dbo.Directors");
            DropIndex("dbo.Contributors", new[] { "CountryId" });
            DropIndex("dbo.Actors", new[] { "Id" });
            DropIndex("dbo.Directors", new[] { "Id" });
            DropPrimaryKey("dbo.Actors");
            DropPrimaryKey("dbo.Directors");
            AddColumn("dbo.Actors", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Actors", "LastName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Actors", "Biography", c => c.String());
            AddColumn("dbo.Actors", "Birthdate", c => c.DateTime());
            AddColumn("dbo.Actors", "Picture", c => c.Binary());
            AddColumn("dbo.Actors", "CountryId", c => c.Int());
            AddColumn("dbo.Directors", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Directors", "LastName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Directors", "Biography", c => c.String());
            AddColumn("dbo.Directors", "Birthdate", c => c.DateTime());
            AddColumn("dbo.Directors", "Picture", c => c.Binary());
            AddColumn("dbo.Directors", "CountryId", c => c.Int());
            AlterColumn("dbo.Actors", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Directors", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Actors", "Id");
            AddPrimaryKey("dbo.Directors", "Id");
            CreateIndex("dbo.Actors", "CountryId");
            CreateIndex("dbo.Directors", "CountryId");
            AddForeignKey("dbo.ActorAwards", "ActorId", "dbo.Actors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MovieActors", "ActorId", "dbo.Actors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Movies", "DirectorId", "dbo.Directors", "Id");
            AddForeignKey("dbo.DirectorAwards", "ADirectorId", "dbo.Directors", "Id", cascadeDelete: true);
            DropTable("dbo.Contributors");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Contributors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Biography = c.String(),
                        Birthdate = c.DateTime(),
                        Picture = c.Binary(),
                        CountryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.DirectorAwards", "ADirectorId", "dbo.Directors");
            DropForeignKey("dbo.Movies", "DirectorId", "dbo.Directors");
            DropForeignKey("dbo.MovieActors", "ActorId", "dbo.Actors");
            DropForeignKey("dbo.ActorAwards", "ActorId", "dbo.Actors");
            DropIndex("dbo.Directors", new[] { "CountryId" });
            DropIndex("dbo.Actors", new[] { "CountryId" });
            DropPrimaryKey("dbo.Directors");
            DropPrimaryKey("dbo.Actors");
            AlterColumn("dbo.Directors", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Actors", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.Directors", "CountryId");
            DropColumn("dbo.Directors", "Picture");
            DropColumn("dbo.Directors", "Birthdate");
            DropColumn("dbo.Directors", "Biography");
            DropColumn("dbo.Directors", "LastName");
            DropColumn("dbo.Directors", "FirstName");
            DropColumn("dbo.Actors", "CountryId");
            DropColumn("dbo.Actors", "Picture");
            DropColumn("dbo.Actors", "Birthdate");
            DropColumn("dbo.Actors", "Biography");
            DropColumn("dbo.Actors", "LastName");
            DropColumn("dbo.Actors", "FirstName");
            AddPrimaryKey("dbo.Directors", "Id");
            AddPrimaryKey("dbo.Actors", "Id");
            CreateIndex("dbo.Directors", "Id");
            CreateIndex("dbo.Actors", "Id");
            CreateIndex("dbo.Contributors", "CountryId");
            AddForeignKey("dbo.DirectorAwards", "ADirectorId", "dbo.Directors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Movies", "DirectorId", "dbo.Directors", "Id");
            AddForeignKey("dbo.MovieActors", "ActorId", "dbo.Actors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ActorAwards", "ActorId", "dbo.Actors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Directors", "Id", "dbo.Contributors", "Id");
            AddForeignKey("dbo.Actors", "Id", "dbo.Contributors", "Id");
        }
    }
}
