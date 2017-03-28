using System.ComponentModel.DataAnnotations;

namespace OnlineSatisProje.Web.Areas.Admin.Models
{
    public class EtiketModel
    {
        public EtiketModel()
        {
        }

        public EtiketModel(string modalId)
        {
            ModalId = modalId;
        }

        [Required]
        [StringLength(50, ErrorMessage = "Maksimum 50 karakter olmalı")]
        public string Ad { get; set; }

        public string ModalId { get; set; }
    }
}