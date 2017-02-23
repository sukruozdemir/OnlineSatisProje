using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("UrunOzellikMapping")]
    public class UrunOzellikMapping : BaseEntity
    {
        public UrunOzellikMapping()
        {
            UrunOzellikDeger = new HashSet<UrunOzellikDeger>();
        }

        public int UrunId { get; set; }
        public int UrunOzellikId { get; set; }
        public int OzellikControlTipId { get; set; }

        public virtual Urun Urun { get; set; }
        public virtual UrunOzellik UrunOzellik { get; set; }
        public virtual ICollection<UrunOzellikDeger> UrunOzellikDeger { get; set; }
    }
}