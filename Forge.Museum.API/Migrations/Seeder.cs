using Forge.Museum.API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Forge.Museum.API.Migrations
{
    public class Seeder
    {
        public static void SeedAll(ApplicationDbContext context)
        {
            context.Artefacts.AddOrUpdate(new Artefact
            {
                Name = "Dynamax Pikachu",
                Description = "How did we fit this inside the Museum",
                Radius_Of_Effect = 200,
                Coord_X = 10,
                Coord_Y = 10,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                AcquisitionDate = DateTime.UtcNow
            });
        }
    }
}