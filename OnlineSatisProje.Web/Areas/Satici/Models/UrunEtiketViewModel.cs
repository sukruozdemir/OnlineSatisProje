using System.Collections.Generic;
using OnlineSatisProje.Core.Entities;

namespace OnlineSatisProje.Web.Areas.Satici.Models
{
    public class UrunEtiketViewModel
    {
        public ICollection<UrunEtiketMapping> UrunEtiketMapping { get; set; }
        public UrunEtiketModel UrunEtiketModel { get; set; }
    }
}