using HoteliNovica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoteliNovica.Interfaces
{
    public interface ILanacHotelRepository
    {
        IEnumerable<LanacHotel> GetAll();
        LanacHotel GetById(int id);
        IEnumerable<LanacDTO> GetStatistics();
        IEnumerable<LanacDTO>SobaPretrage(int granice);
    }
}
