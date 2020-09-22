using Forge.Museum.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forge.Museum.Web.Models
{
    public partial class Beacon
    {
        public int Id { get; set; }

        [Required]
        public int Visits { get; set; }
        public string Name { get; set; }
        public double Coord_X { get; set; }
        public double Coord_Y { get; set; }
        public string Description { get; set; }
        public string MAC_address { get; set; }

        public virtual ICollection<Artefact> Artefacts { get; set; }
    }
}