using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("Adres")]
    public class Adres : BaseEntity
    {
        public Adres()
        {
            KullaniciAdresMapping = new HashSet<KullaniciAdresMapping>();
            SaticiMekanMapping = new HashSet<SaticiMekanMapping>();
            Siparis = new HashSet<Siparis>();
            CreatedDate = DateTime.Now;
        }

        [Required]
        [StringLength(150)]
        public string Baslik { get; set; }

        [Column("Adres")]
        [Required]
        [StringLength(250)]
        public string Adres1 { get; set; }

        [Display(Name = "Ýlçe")]
        public int IlceId { get; set; }

        [StringLength(15)]
        public string Telefon { get; set; }

        public bool Aktif { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Ilce Ilce { get; set; }

        public virtual ICollection<KullaniciAdresMapping> KullaniciAdresMapping { get; set; }

        public virtual ICollection<SaticiMekanMapping> SaticiMekanMapping { get; set; }

        public virtual ICollection<Siparis> Siparis { get; set; }
    }
}