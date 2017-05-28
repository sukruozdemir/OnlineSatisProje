using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineSatisProje.Core.Enums;

namespace OnlineSatisProje.Core.Entities
{
    public class Siparis : BaseEntity
    {
        public Siparis()
        {
            SiparisItem = new HashSet<SiparisItem>();
        }

        public Guid SiparisGuid { get; set; }

        [Display(Name = "Kullanýcý")]
        public string KullaniciId { get; set; }

        [Display(Name = "Kargo adresi")]
        public int KargoAdresId { get; set; }

        [Display(Name = "Sipariþ durumu")]
        public int? SiparisDurumId { get; set; }

        [Display(Name = "Ödeme durumu")]
        public int? OdemeDurumId { get; set; }

        [Display(Name = "Kargo durumu")]
        public int? KargoDurumId { get; set; }

        [Display(Name = "Sipariþ toplamý")]
        public decimal SiparisToplam { get; set; }

        [Display(Name = "Kart adý")]
        public string KartAdi { get; set; }

        [Display(Name = "Kart numarasý")]
        public string KartNumarasi { get; set; }

        [Display(Name = "CVV")]
        public string KartCvv { get; set; }

        [Display(Name = "Ay")]
        public string KartSonKullanimAy { get; set; }

        [Display(Name = "Yýl")]
        public string KartSonKullanimYil { get; set; }

        public bool Aktif { get; set; }
        public bool Silindi { get; set; }
        public DateTime Tarih { get; set; }

        [NotMapped]
        public SiparisDurumu SiparisDurumu
        {
            get { return (SiparisDurumu) SiparisDurumId; }
            set { SiparisDurumId = (int) value; }
        }

        [NotMapped]
        public OdemeDurumu OdemeDurumu
        {
            get { return (OdemeDurumu) OdemeDurumId; }
            set { OdemeDurumId = (int) value; }
        }

        [NotMapped]
        public KargoDurumu KargoDurumu
        {
            get { return (KargoDurumu) KargoDurumId; }
            set { KargoDurumId = (int) value; }
        }

        public virtual Kullanici Kullanici { get; set; }
        public virtual Adres Adres { get; set; }
        public virtual ICollection<SiparisItem> SiparisItem { get; set; }
    }
}