using Forge.Museum.API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;

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
                AcquisitionDate = DateTime.UtcNow,
                Image = getSeedImage(),
                ImageFileType = ".jpg"
            });
        }
        private static byte[] getSeedImage()
        {
            
            using (Image image = Image.FromFile(MapPath("/Content/Images/pokemon.jpg")))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    //byte[] data = Convert.FromBase64String(Quot[i].imageFile.Replace("data:image/jpeg;base64,", ""));
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    return imageBytes;
                }
            }
        }
        private static string MapPath(string seedFile)
        {
            if (HttpContext.Current != null)
                return HostingEnvironment.MapPath(seedFile);

            var absolutePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath; //was AbsolutePath but didn't work with spaces according to comments
            var directoryName = Path.GetDirectoryName(absolutePath);
            var path = Path.Combine(directoryName, ".." + seedFile.TrimStart('~').Replace('/', '\\'));

            return path;
        }
    }
    
}