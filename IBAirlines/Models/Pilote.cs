using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IBAirlines.Models
{
    public class Pilote
    {
        public int PiloteID { get; set; }

        [Required(ErrorMessage = "Nom est requis"), MaxLength(50)]
        public string Nom { get; set; }

        [Required(ErrorMessage = "PreNom est requis"), MaxLength(50)]
        public string PreNom { get; set; }

        [Required(ErrorMessage = "Adresse est requise"), MaxLength(30)]
        public string Adresse { get; set; }

        [Required(ErrorMessage = "Code Postal est requis"), MaxLength(10)]
        [DataType(DataType.PostalCode)]
        public string CodePostal { get; set; }

        [Required(ErrorMessage = "Ville est requise"), MaxLength(20)]
        public string Ville { get; set; }

        [Required(ErrorMessage = "Le numéro de téléphone est requis")]
        [DataType(DataType.PhoneNumber)]
        public string Tel { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime DateNaissance { get; set; }

        [Required(ErrorMessage = "Salaire est requis")]
        [Range(0, double.MaxValue)]
        public double Salaire { get; set; }
    }
}
