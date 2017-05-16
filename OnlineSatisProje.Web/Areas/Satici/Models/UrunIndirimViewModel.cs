using OnlineSatisProje.Core.Entities;
using System.Collections.Generic;

namespace OnlineSatisProje.Web.Areas.Satici.Models
{
    public class UrunIndirimViewModel
    {
        public List<UrunIndirimMapping> UrunIndirimMapping { get; set; }
        public UrunIndirimModel UrunIndirimModel { get; set; }
    }
}