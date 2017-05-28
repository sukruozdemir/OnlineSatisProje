using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineSatisProje.Core.Entities;

namespace OnlineSatisProje.Web.Areas.Satici.Models
{
    public class UrunKategoriModel
    {
        [Display(Name = "Ürün")]
        [Required]
        public int UrunId { get; set; }

        [Display(Name = "Kategori")]
        [Required]
        public int KategoriId { get; set; }

        public Urun Urun { get; set; }
        public IEnumerable<Kategori> Kategoriler { get; set; }
    }
}