using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace Forge.Museum.Web.Models
{

    public partial class Artefact
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        [StringLength(4000)]
        public string AdditionalComments { get; set; }

        public DateTime AcquisitionDate { get; set; }

        public double Radius_Of_Effect { get; set; }

        public int ArtefactStatus { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

        public int? ArtefactCategory_Id { get; set; }

        public int? Zone_Id { get; set; }
        public int? Beacon_Id { get; set; }

        public virtual ArtefactCategory ArtefactCategory { get; set; }
    }
}
