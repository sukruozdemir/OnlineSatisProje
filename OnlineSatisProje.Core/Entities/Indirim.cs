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
            KategoriIndirimMapping = new HashSet<KategoriIndirimMapping>();
            SaticiIndirimMapping = new HashSet<SaticiIndirimMapping>();
            UrunIndirimMapping = new HashSet<UrunIndirimMapping>();
        }

        [StringLength(50)]
        public string Baslik { get; set; }

        public bool YuzdeKullan { get; set; }
        public decimal IndirimYuzdesi { get; set; }
        public decimal IndirimMiktari { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public bool Aktif { get; set; }

        public virtual ICollection<KategoriIndirimMapping> KategoriIndirimMapping { get; set; }
        public virtual ICollection<SaticiIndirimMapping> SaticiIndirimMapping { get; set; }
        public virtual ICollection<UrunIndirimMapping> UrunIndirimMapping { get; set; }
    }
}