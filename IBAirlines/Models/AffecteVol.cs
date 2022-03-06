using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IBAirlines.Models
{
    public class AffecteVol
    {
        public int AffecteVolID { get; set; }

        [ForeignKey("Passager")]
        public int PassagerID { get; set; }
        public virtual Passager Passager { get; set; }

        [ForeignKey("Vol")]
        public int VolID { get; set; }
        public virtual Vol Vol { get; set; }

        [Required(ErrorMessage = "Date de vol est requise")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime DateVol { get; set; }

        [Required(ErrorMessage = "Numéro de place est requis"), MaxLength(10)]
        public string NumPlace { get; set; }

        [Range(0, double.MaxValue)]
        public double Prix { get; set; }
    }
}
