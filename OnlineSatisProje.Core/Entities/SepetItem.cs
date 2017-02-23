using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("SepetItem")]
    public class SepetItem : BaseEntity
    {
        public SepetItem()
        {
            CreatedDate = DateTime.Now;
            UpdatedDate = CreatedDate;
        }

        public int KullaniciId { get; set; }
        public int UrunId { get; set; }
        public int Miktar { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Urun Urun { get; set; }
        public virtual Kullanici Kullanici { get; set; }
    }
}