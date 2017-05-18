using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace OnlineSatisProje.Core.Entities
{
    [Table("Indirim")]
    public class Indirim : BaseEntity
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Indirim()
        {
            SaticiIndirimMapping = new HashSet<SaticiIndirimMapping>();
            UrunIndirimMapping = new HashSet<UrunIndirimMapping>();
        }

        [StringLength(50)]
        [Display(Name = "Baþlýk")]
        public string Baslik { get; set; }
        [Display(Name = "Yüzde kullan")]
        public bool YuzdeKullan { get; set; }
        [Display(Name = "Indirim yüzdesi")]
        public int IndirimYuzdesi { get; set; }
        [Display(Name = "Indirim miktarý")]
        public int IndirimMiktari { get; set; }
        [Display(Name = "Baþlangýç tarihi")]
        public DateTime BaslangicTarihi { get; set; }
        [Display(Name = "Bitiþ tarihi")]
        public DateTime BitisTarihi { get; set; }
        public bool Aktif { get; set; }

        public virtual ICollection<SaticiIndirimMapping> SaticiIndirimMapping { get; set; }
        public virtual ICollection<UrunIndirimMapping> UrunIndirimMapping { get; set; }
    }
}