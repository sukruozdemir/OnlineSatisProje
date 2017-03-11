using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OnlineSatisProje.Data;
using OnlineSatisProje.Services.Interfaces;

namespace OnlineSatisProje.Services.Services
{
    public class RolRepository : IRolRepository
    {
        private readonly IIdentityRepostitory _identityRepostitory;
        private readonly IRepository<IdentityRole> _repository;

        public RolRepository(IRepository<IdentityRole> repository, IIdentityRepostitory identityRepostitory)
        {
            _repository = repository;
            _identityRepostitory = identityRepostitory;
        }

        public List<IdentityRole> GetRoles()
        {
            var list = _repository.Table.ToList();
            return list;
        }

        public bool RolEkle(string email, string rolId)
        {
            try
            {
                var user = _identityRepostitory.UserManager.FindByEmail(email);
                var role = _repository.GetById(rolId);
                var result = _identityRepostitory.UserManager.AddToRole(user.Id, role.Name);
                return result.Succeeded;
            }
            catch
            {
                return false;
            }
        }
    }
}