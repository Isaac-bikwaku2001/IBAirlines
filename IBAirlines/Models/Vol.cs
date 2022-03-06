using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IBAirlines.Models
{
    public class Vol
    {
        public int VolID { get; set; }

        [ForeignKey("Avion")]
        public int AvionID { get; set; }
        public virtual Avion Avion { get; set; }

        [ForeignKey("Pilote")]
        public int PiloteID { get; set; }
        public virtual Pilote Pilote { get; set; }

        [Required(ErrorMessage = "Ville de départ est requis"), MaxLength(26)]
        public string VilleDepart { get; set; }

        [Required(ErrorMessage = "Ville d'arrivée est requis"), MaxLength(26)]
        public string VilleArrivee { get; set; }
        
        [Required(ErrorMessage = "Heure de départ est requise")]
        public TimeOnly HeureDepart { get; set; }

        [Required(ErrorMessage = "Heure d'arrivée est requise")]
        public TimeOnly HeureArrivee { get; set; }
    }
}
