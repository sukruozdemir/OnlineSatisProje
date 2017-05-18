using OnlineSatisProje.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineSatisProje.Web.Areas.Satici.Models
{
    public class UrunEtiketModel
    {
        [Display(Name = "Ürün")]
        [Required]
        public int UrunId { get; set; }
        [Display(Name = "Etiket")]
        [Required]
        public int EtiketId { get; set; }

        public Urun Urun { get; set; }
        public IEnumerable<Etiket> Etiketler { get; set; }
    }
}