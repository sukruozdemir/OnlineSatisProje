using OnlineSatisProje.Core.Entities;
using System.Collections.Generic;

namespace OnlineSatisProje.Web.Areas.Satici.Models
{
    public class UrunEtiketViewModel
    {
        public ICollection<UrunEtiketMapping> UrunEtiketMapping { get; set; }
        public UrunEtiketModel UrunEtiketModel { get; set; }
    }
}