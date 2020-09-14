namespace Forge.Museum.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedXYActivation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artefacts", "Coord_X", c => c.Double(nullable: false));
            AddColumn("dbo.Artefacts", "Coord_Y", c => c.Double(nullable: false));
            AddColumn("dbo.Artefacts", "Activation", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artefacts", "Activation");
            DropColumn("dbo.Artefacts", "Coord_Y");
            DropColumn("dbo.Artefacts", "Coord_X");
        }
    }
}
