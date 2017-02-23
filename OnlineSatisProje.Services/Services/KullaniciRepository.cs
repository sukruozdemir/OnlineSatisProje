using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Services.Interfaces;

namespace OnlineSatisProje.Services.Services
{
    public class KullaniciRepository : IKullaniciRepository
    {
        private readonly ApplicationContext _db = new ApplicationContext();

        public IEnumerable<Kullanici> GetAll()
        {
            return null;
        }
    }
}
