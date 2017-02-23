using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("Log")]
    public class Log : BaseEntity
    {
        [Required]
        [StringLength(250)]
        public string Action { get; set; }
    }
}