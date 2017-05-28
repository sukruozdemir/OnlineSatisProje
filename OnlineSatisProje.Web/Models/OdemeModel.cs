using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using OnlineSatisProje.Core.Entities;

namespace OnlineSatisProje.Web.Models
{
    public class OdemeModel
    {
        [Display(Name = "Toplam ücret")]
        public decimal ToplamUcret { get; set; }

        [Display(Name = "Kart Üzerindeki Ad")]
        [Required(ErrorMessage = "{0} alanı gereklidir")]
        public string KartAd { get; set; }

        [Display(Name = "Kart Numarası")]
        [Required(ErrorMessage = "{0} alanı gereklidir")]
        [DataType(DataType.CreditCard)]
        public string KartNumara { get; set; }

        [Display(Name = "CVV2")]
        [Required(ErrorMessage = "{0} alanı gereklidir")]
        public string KartCvv { get; set; }

        [Display(Name = "Son Kullanım Ay")]
        [Required(ErrorMessage = "{0} alanı gereklidir")]
        public string KartSonKullanimAy { get; set; }

        [Display(Name = "Son Kullanım Yıl")]
        [Required(ErrorMessage = "{0} alanı gereklidir")]
        public string KartSonKullanimYil { get; set; }

        [Display(Name = "Kargo adresi")]
        [Required(ErrorMessage = "{0} alanı gereklidir")]
        public int KargoAdresi { get; set; }

        public Kullanici CurrentUser { private get; set; }

        public IEnumerable<SelectListItem> Yillar
        {
            get
            {
                var liste = new List<SelectListItem>();
                var tarih = DateTime.Now.Year - 1;
                for (var i = 1; i <= 10; i++)
                {
                    var yil = tarih + i;
                    liste.Add(new SelectListItem
                    {
                        Text = yil.ToString(),
                        Value = yil.ToString()
                    });
                }
                return liste;
            }
        }

        public IEnumerable<SelectListItem> Aylar
        {
            get
            {
                var liste = new List<SelectListItem>();
                for (var i = 1; i <= 12; i++)
                    liste.Add(new SelectListItem
                    {
                        Text = i.ToString(),
                        Value = i.ToString()
                    });
                return liste;
            }
        }

        public IEnumerable<SelectListItem> Adresler
        {
            get
            {
                return CurrentUser.KullaniciAdresMapping.Select(kullaniciAdresMapping => new SelectListItem
                    {
                        Text = kullaniciAdresMapping.Adres.Baslik,
                        Value = kullaniciAdresMapping.AdresId.ToString()
                    })
                    .ToList();
            }
        }
    }
}