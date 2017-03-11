using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlineSatisProje.Services.Interfaces
{
    public interface IRolRepository
    {
        List<IdentityRole> GetRoles();

        bool RolEkle(string userId, string rolId);
    }
}
