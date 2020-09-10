namespace Forge.Museum.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArtefactChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artefacts", "Radius_Of_Effect", c => c.Double(nullable: false));
            DropColumn("dbo.Artefacts", "Measurement_Length");
            DropColumn("dbo.Artefacts", "Measurement_Width");
            DropColumn("dbo.Artefacts", "Measurement_Height");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Artefacts", "Measurement_Height", c => c.Int(nullable: false));
            AddColumn("dbo.Artefacts", "Measurement_Width", c => c.Int(nullable: false));
            AddColumn("dbo.Artefacts", "Measurement_Length", c => c.Int(nullable: false));
            DropColumn("dbo.Artefacts", "Radius_Of_Effect");
        }
    }
}
