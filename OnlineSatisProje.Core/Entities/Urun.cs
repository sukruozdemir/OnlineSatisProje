using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("Urun")]
    public class Urun : BaseEntity
    {
        public Urun()
        {
            SepetItem = new HashSet<SepetItem>();
            SiparisItem = new HashSet<SiparisItem>();
            UrunEtiketMapping = new HashSet<UrunEtiketMapping>();
            UrunIndirimMapping = new HashSet<UrunIndirimMapping>();
            UrunKategoriMapping = new HashSet<UrunKategoriMapping>();
            UrunOzellikMapping = new HashSet<UrunOzellikMapping>();
            UrunResimMapping = new HashSet<UrunResimMapping>();
            CreatedDate = DateTime.Now;
            UpdatedDate = CreatedDate;
        }

        [Required]
        [StringLength(250)]
        public string Baslik { get; set; }

        [Required]
        public string KisaAciklama { get; set; }

        [Required]
        public string TamAciklama { get; set; }

        public bool AnasayfadaGoster { get; set; }
        public bool KullaniciYorumIzinVer { get; set; }
        public bool KargoAktif { get; set; }
        public bool UcretsizKargo { get; set; }
        public decimal Fiyat { get; set; }
        public bool Aktif { get; set; }
        public bool Silindi { get; set; }
        public bool Yayinlandi { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int SaticiId { get; set; }

        [ForeignKey("SaticiId")]
        public virtual Satici Satici { get; set; }

        public virtual ICollection<SepetItem> SepetItem { get; set; }
        public virtual ICollection<SiparisItem> SiparisItem { get; set; }
        public virtual ICollection<UrunEtiketMapping> UrunEtiketMapping { get; set; }
        public virtual ICollection<UrunIndirimMapping> UrunIndirimMapping { get; set; }
        public virtual ICollection<UrunKategoriMapping> UrunKategoriMapping { get; set; }
        public virtual ICollection<UrunOzellikMapping> UrunOzellikMapping { get; set; }
        public virtual ICollection<UrunResimMapping> UrunResimMapping { get; set; }
    }
}