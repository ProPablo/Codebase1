using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forge.Museum.API.Models
{
    public class Beacon : Base
    {
        public Beacon()
        {

        }
        public int Id { get; set; }
        public int Visits { get; set; }
        public string Name { get; set; }
         //"serviceData": {"0000fe6a-0000-1000-8000-00805f9b34fb": "AgYBFmT0U3RESEdn"}
        public double Coord_X { get; set; }
        public double Coord_Y { get; set; }
        public string Description { get; set; }

        [Required, StringLength(17)]
        public string MAC_address { get; set; }
        
        public virtual ICollection<Artefact> Artefacts { get; set; }
    }
}