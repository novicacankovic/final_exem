using HoteliNovica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoteliNovica.Interfaces
{
   public interface IHotelRepository
    {
        IEnumerable<Hotel> GetAll();
        Hotel GetById(int id);
        void Add(Hotel hotel);
        void Update(Hotel hotel);
        void Delete(Hotel hotel);
    }
}
