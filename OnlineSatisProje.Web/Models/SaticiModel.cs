using System.ComponentModel.DataAnnotations;

namespace OnlineSatisProje.Web.Models
{
    public class SaticiModel
    {
        [Required(ErrorMessage = "{0} alanı zorunlu alandır")]
        public string Ad { get; set; }

        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }

        [Display(Name = "Email adresi")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}