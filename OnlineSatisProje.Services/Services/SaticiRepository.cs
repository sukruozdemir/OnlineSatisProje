using System.Collections.Generic;
using System.Linq;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Services.Interfaces;

namespace OnlineSatisProje.Services.Services
{
    public class SaticiRepository : ISaticiRepository
    {
        private readonly IRepository<Satici> _saticiRepository;

        public SaticiRepository(IRepository<Satici> saticiRepository)
        {
            _saticiRepository = saticiRepository;
        }

        public IEnumerable<Satici> GetAvailableSaitiSaticis()
        {
            var liste = _saticiRepository.Table.Where(s => s.Aktif && !s.Silindi).ToList();
            return liste;
        }
    }
}