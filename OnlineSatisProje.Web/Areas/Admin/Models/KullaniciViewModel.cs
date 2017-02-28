using System.Collections.Generic;
using OnlineSatisProje.Core.Entities;

namespace OnlineSatisProje.Web.Areas.Admin.Models
{
    public class KullaniciViewModel
    {
        public Kullanici Kullanici { get; set; }
        public IList<string> Roles { get; set; }
    }
}