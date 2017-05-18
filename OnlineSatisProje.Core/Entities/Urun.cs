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
            UrunResimMapping = new HashSet<UrunResimMapping>();
            CreatedDate = DateTime.Now;
            UpdatedDate = CreatedDate;
        }

        [Required(ErrorMessage = "{0} alan� zorunludur")]
        [Display(Name = "Ba�l�k")]
        [StringLength(250)]
        public string Baslik { get; set; }

        [Required(ErrorMessage = "{0} alan� zorunludur")]
        [Display(Name = "K�sa a��klama")]
        [MaxLength(300, ErrorMessage = "{0} alan� maksimum {1} karakter olabilir")]
        public string KisaAciklama { get; set; }

        [Required(ErrorMessage = "{0} alan� zorunludur")]
        [Display(Name = "Tam a��klama")]
        [MaxLength(Int32.MaxValue)]
        public string TamAciklama { get; set; }

        [Display(Name = "Anasayfada g�ster")]
        public bool AnasayfadaGoster { get; set; }
        [Display(Name = "Kullan�c� yorumlar�na izin ver")]
        public bool KullaniciYorumIzinVer { get; set; }
        [Display(Name = "Kargo aktif")]
        public bool KargoAktif { get; set; }
        [Display(Name = "�cretsiz kargo")]
        public bool UcretsizKargo { get; set; }
        public decimal Fiyat { get; set; }
        public bool Aktif { get; set; }
        public bool Silindi { get; set; }
        [Display(Name = "Yay�nland�")]
        public bool Yayinlandi { get; set; }
        [Display(Name = "Olu�turulma tarihi")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "G�ncellenme tarihi")]
        public DateTime UpdatedDate { get; set; }
        [Display(Name = "Sat�c�")]
        public int SaticiId { get; set; }

        [ForeignKey("SaticiId")]
        public virtual Satici Satici { get; set; }

        public virtual ICollection<SepetItem> SepetItem { get; set; }
        public virtual ICollection<SiparisItem> SiparisItem { get; set; }
        public virtual ICollection<UrunEtiketMapping> UrunEtiketMapping { get; set; }
        public virtual ICollection<UrunIndirimMapping> UrunIndirimMapping { get; set; }
        public virtual ICollection<UrunKategoriMapping> UrunKategoriMapping { get; set; }
        public virtual ICollection<UrunResimMapping> UrunResimMapping { get; set; }
    }
}