using System.Collections.Generic;
using OnlineSatisProje.Core.Entities;

namespace OnlineSatisProje.Web.Areas.Satici.Models
{
    public class UrunIndirimViewModel
    {
        public List<UrunIndirimMapping> UrunIndirimMapping { get; set; }
        public UrunIndirimModel UrunIndirimModel { get; set; }
    }
}