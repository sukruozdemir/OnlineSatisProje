using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("SaticiMekanMapping")]
    public class SaticiMekanMapping : BaseEntity
    {
        public SaticiMekanMapping()
        {
            CreatedDate = DateTime.Now;
            UpdatedDate = CreatedDate;
        }

        public int SaticiId { get; set; }
        public int AdresId { get; set; }

        [StringLength(500)]
        public string Aciklama { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Adres Adres { get; set; }
        public virtual Satici Satici { get; set; }
    }
}