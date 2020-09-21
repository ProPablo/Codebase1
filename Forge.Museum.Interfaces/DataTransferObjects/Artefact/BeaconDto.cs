using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forge.Museum.Interfaces.DataTransferObjects.Artefact
{
    public class BeaconDto : BaseDto
    {
        public int Id { get; set; }
        public int Visits { get; set; }
        public string Name { get; set; }
        public double Coord_X { get; set; }
        public double Coord_Y { get; set; }
        public string Description { get; set; }
        public string MAC_address { get; set; }
        public List<ArtefactDto> Artefacts { get; set; }
    }
}
