using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("KullaniciAdresMapping")]
    public class KullaniciAdresMapping : BaseEntity
    {
        public int KullaniciId { get; set; }
        public int AdresId { get; set; }

        public virtual Kullanici Kullanici { get; set; }
        public virtual Adres Adres { get; set; }
    }
}