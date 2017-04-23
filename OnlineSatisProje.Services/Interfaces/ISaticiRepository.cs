using System.Collections.Generic;
using OnlineSatisProje.Core.Entities;

namespace OnlineSatisProje.Services.Interfaces
{
    public interface ISaticiRepository
    {
        IEnumerable<Satici> GetAvailableSaitiSaticis();
    }
}
