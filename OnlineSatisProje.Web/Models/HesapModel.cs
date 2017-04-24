using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineSatisProje.Web.Models
{
    public class HesapModel
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Kullanıcı adı")]
        public string KullaniciAdi { get; set; }
        [Display(Name = "Satıcı mı")]
        public bool SaticiMi { get; set; }
        [Display(Name = "Oluşturulma tarihi")]
        public DateTime CreatedDate { get; set; }
    }
}