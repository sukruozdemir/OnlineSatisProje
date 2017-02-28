using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlineSatisProje.Core.Entities
{
    public class Kullanici : IdentityUser
    {
        public Kullanici()
        {
            CreatedDate = DateTime.Now;
            Siparis = new HashSet<Siparis>();
            KullaniciAdresMapping = new HashSet<KullaniciAdresMapping>();
            SepetItem = new HashSet<SepetItem>();
        }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Siparis> Siparis { get; set; }
        public virtual ICollection<KullaniciAdresMapping> KullaniciAdresMapping { get; set; }
        public virtual ICollection<SepetItem> SepetItem { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Kullanici> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}