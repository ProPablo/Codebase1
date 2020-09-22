namespace Forge.Museum.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Beacons : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Beacons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Visits = c.Int(nullable: false),
                        Name = c.String(),
                        Coord_X = c.Double(nullable: false),
                        Coord_Y = c.Double(nullable: false),
                        Description = c.String(),
                        MAC_address = c.String(nullable: false, maxLength: 17),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Artefacts", "Beacon_Id", c => c.Int());
            CreateIndex("dbo.Artefacts", "Beacon_Id");
            AddForeignKey("dbo.Artefacts", "Beacon_Id", "dbo.Beacons", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Artefacts", "Beacon_Id", "dbo.Beacons");
            DropIndex("dbo.Artefacts", new[] { "Beacon_Id" });
            DropColumn("dbo.Artefacts", "Beacon_Id");
            DropTable("dbo.Beacons");
        }
    }
}
