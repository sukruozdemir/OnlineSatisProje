using OnlineSatisProje.Core.Entities;
using System.Collections.Generic;

namespace OnlineSatisProje.Web.Areas.Satici.Models
{
    public class UrunKategoriViewModel
    {
        public ICollection<UrunKategoriMapping> UrunKategoriMapping { get; set; }
        public UrunKategoriModel UrunKategoriModel { get; set; }
    }
}