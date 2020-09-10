namespace Forge.Museum.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueCodeRemove : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Artefacts", "UniqueCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Artefacts", "UniqueCode", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
