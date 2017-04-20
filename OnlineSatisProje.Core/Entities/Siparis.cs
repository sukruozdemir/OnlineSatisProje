using System;
using System.Collections.Generic;
using OnlineSatisProje.Core.Enums;

namespace OnlineSatisProje.Core.Entities
{
    public class Siparis : BaseEntity
    {
        public Siparis()
        {
            Kargo = new HashSet<Kargo>();
            SiparisItem = new HashSet<SiparisItem>();
        }

        public Guid SiparisGuid { get; set; }
        public string KullaniciId { get; set; }
        public int KargoAdresId { get; set; }
        public int? SiparisDurumId { get; set; }
        public int? OdemeDurumId { get; set; }
        public int? KargoDurumId { get; set; }
        public decimal SiparisToplam { get; set; }
        public string KartAdi { get; set; }
        public string KartNumarasi { get; set; }
        public string KartCvv { get; set; }
        public string KartSonKullanimAy { get; set; }
        public string KartSonKullanimYil { get; set; }
        public bool Aktif { get; set; }
        public bool Silindi { get; set; }
        public DateTime Tarih { get; set; }

        public SiparisDurumu SiparisDurumu
        {
            get
            {
                return (SiparisDurumu)SiparisDurumId;
            }
            set { SiparisDurumId = (int)value; }
        }

        public OdemeDurumu OdemeDurumu
        {
            get
            {
                return (OdemeDurumu)OdemeDurumId;
            }
            set { OdemeDurumId = (int)value; }
        }

        public KargoDurumu KargoDurumu
        {
            get { return (KargoDurumu)KargoDurumId; }
            set
            {
                KargoDurumId = (int)value;
            }
        }

        public virtual Kullanici Kullanici { get; set; }
        public virtual Adres Adres { get; set; }
        public virtual ICollection<Kargo> Kargo { get; set; }
        public virtual ICollection<SiparisItem> SiparisItem { get; set; }
    }
}