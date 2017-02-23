using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("UrunOzellik")]
    public class UrunOzellik : BaseEntity
    {
        public UrunOzellik()
        {
            UrunOzellikMapping = new HashSet<UrunOzellikMapping>();
        }

        [Required]
        [StringLength(250)]
        public string Ad { get; set; }

        public virtual ICollection<UrunOzellikMapping> UrunOzellikMapping { get; set; }
    }
}