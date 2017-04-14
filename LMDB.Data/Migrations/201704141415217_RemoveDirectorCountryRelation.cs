namespace LMDB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDirectorCountryRelation : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Directors", name: "CountryId", newName: "Country_Id");
            RenameIndex(table: "dbo.Directors", name: "IX_CountryId", newName: "IX_Country_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Directors", name: "IX_Country_Id", newName: "IX_CountryId");
            RenameColumn(table: "dbo.Directors", name: "Country_Id", newName: "CountryId");
        }
    }
}
