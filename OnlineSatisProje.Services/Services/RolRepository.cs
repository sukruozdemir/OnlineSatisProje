using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Services.Interfaces;

namespace OnlineSatisProje.Services.Services
{
    public class RolRepository : IRolRepository
    {
        private readonly IIdentityRepostitory _identityRepostitory;
        private readonly IRepository<IdentityRole> _repository;
        private readonly IRepository<Satici> _saticiRepository;

        public RolRepository(IRepository<IdentityRole> repository,
            IIdentityRepostitory identityRepostitory,
            IRepository<Satici> saticiRepository)
        {
            _repository = repository;
            _identityRepostitory = identityRepostitory;
            _saticiRepository = saticiRepository;
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
                if (!result.Succeeded)
                    return result.Succeeded;
                if (role.Name != "Satıcı")
                    return result.Succeeded;
                var satici = new Satici
                {
                    KullaniciId = user.Id
                };
                _saticiRepository.Insert(satici);
                return result.Succeeded;
            }
            catch
            {
                return false;
            }
        }
    }
}