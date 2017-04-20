using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("Satici")]
    public class Satici : BaseEntity
    {
        public Satici()
        {
            SaticiEtiketMapping = new HashSet<SaticiEtiketMapping>();
            SaticiIndirimMapping = new HashSet<SaticiIndirimMapping>();
            SaticiMekanMapping = new HashSet<SaticiMekanMapping>();
            Uruns = new HashSet<Urun>();
            SiparisItems = new HashSet<SiparisItem>();
            CreatedDate = DateTime.Now;
            UpdatedDate = CreatedDate;
        }

        public string KullaniciId { get; set; }

        [StringLength(150)]
        public string Ad { get; set; }

        [StringLength(1000)]
        public string Aciklama { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public int? LogoId { get; set; }
        public bool? Aktif { get; set; }
        public bool? Silindi { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public Kullanici Kullanici { get; set; }

        public virtual ICollection<SaticiEtiketMapping> SaticiEtiketMapping { get; set; }
        public virtual ICollection<SaticiIndirimMapping> SaticiIndirimMapping { get; set; }
        public virtual ICollection<SaticiMekanMapping> SaticiMekanMapping { get; set; }
        public virtual ICollection<Urun> Uruns { get; set; }
        public virtual ICollection<SiparisItem> SiparisItems { get; set; }
    }
}