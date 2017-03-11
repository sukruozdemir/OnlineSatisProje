using System.ComponentModel.DataAnnotations;

namespace OnlineSatisProje.Web.Areas.Admin.Models
{
    public class KullaniciRolModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Rol Adı")]
        public string RolId { get; set; }
    }
}