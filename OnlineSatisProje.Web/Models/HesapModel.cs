using System;

namespace OnlineSatisProje.Web.Models
{
    public class HesapModel
    {
        public string Email { get; set; }
        public string KullaniciAdi { get; set; }
        public bool SaticiMi { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}