using System.ComponentModel.DataAnnotations;

namespace IBAirlines.Models
{
    public class Passager
    {
        public int PassagerID { get; set; }

        [Required(ErrorMessage = "Nom est requis"), MaxLength(50)]
        public string Nom { get; set; }

        [Required(ErrorMessage = "PreNom est requis"), MaxLength(50)]
        public string PreNom { get; set; }

        [Required(ErrorMessage = "Ville est requise"), MaxLength(20)]
        public string Ville { get; set; }
    }
}
