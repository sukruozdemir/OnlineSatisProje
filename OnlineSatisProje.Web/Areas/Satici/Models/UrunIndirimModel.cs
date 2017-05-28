using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineSatisProje.Core.Entities;

namespace OnlineSatisProje.Web.Areas.Satici.Models
{
    public class UrunIndirimModel
    {
        [Display(Name = "Ürün")]
        [Required]
        public int UrunId { get; set; }

        [Display(Name = "İndirim")]
        [Required]
        public int IndirimId { get; set; }

        public Urun Urun { get; set; }
        public IEnumerable<Indirim> Indirimler { get; set; }
    }
}