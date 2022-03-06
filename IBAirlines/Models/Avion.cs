using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IBAirlines.Models
{
    public class Avion
    {
        public int AvionID { get; set; }

        [Required(ErrorMessage = "Marque est requise"), MaxLength(10)]
        public string Marque { get; set; }

        [Required(ErrorMessage = "Type est requis"), MaxLength(15)]
        public string Type { get; set; }

        [Required(ErrorMessage = "Capacité est requise")]
        [Range(0, int.MaxValue)]
        public int Capacite { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime DateMiseEnService { get; set; }
    }
}
