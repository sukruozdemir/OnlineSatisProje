using System.Collections.Generic;
using OnlineSatisProje.Core.Entities;

namespace OnlineSatisProje.Web.Areas.Satici.Models
{
    public class UrunKategoriViewModel
    {
        public ICollection<UrunKategoriMapping> UrunKategoriMapping { get; set; }
        public UrunKategoriModel UrunKategoriModel { get; set; }
    }
}