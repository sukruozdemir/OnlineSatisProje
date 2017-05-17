using OnlineSatisProje.Core.Entities;

namespace OnlineSatisProje.Web.Models
{
    public class UrunViewModel
    {
        public decimal Fiyat { get; set; }
        public bool IndirimVar { get; set; }
        public Urun Urun { get; set; }
    }
}