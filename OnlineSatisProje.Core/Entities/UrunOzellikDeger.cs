using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("UrunOzellikDeger")]
    public class UrunOzellikDeger : BaseEntity
    {
        public int UrunOzellikMappingId { get; set; }
        public int OzellikDegerTipId { get; set; }

        [Required]
        [StringLength(250)]
        public string Ad { get; set; }

        public virtual UrunOzellikMapping UrunOzellikMapping { get; set; }
    }
}