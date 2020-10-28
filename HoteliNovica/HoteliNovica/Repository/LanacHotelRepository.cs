using HoteliNovica.Interfaces;
using HoteliNovica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoteliNovica.Repository
{
    public class LanacHotelRepository : IDisposable, ILanacHotelRepository
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<LanacHotel> GetAll()
        {
            return db.LanacHotel;
        }

        public LanacHotel GetById(int id)
        {
            return db.LanacHotel.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<LanacDTO> GetStatistics()
        {
            var lanci = (from lanacHotel in db.LanacHotel
                         join hotel in db.Hotel on lanacHotel.Id equals hotel.LanacHotelId
                         group new { lanacHotel, hotel } by new { lanacHotel.Id, lanacHotel.Naziv, lanacHotel.GodinaOsnivanja } into g
                         select new LanacDTO
                         {
                             Id = g.Key.Id,
                             Naziv = g.Key.Naziv,
                             GodinaOsnivanja = g.Key.GodinaOsnivanja,
                             Prosek = g.Average(x => (decimal)x.hotel.BrojZaposlenih)
                         }).ToList().OrderByDescending(x => x.Prosek);

            return lanci;
        }

        public IEnumerable<LanacDTO> SobaPretrage(int granica)
        {
            var lanci1 = (from lanacHotel in db.LanacHotel
                          join hotel in db.Hotel on lanacHotel.Id equals hotel.LanacHotelId
                          group new { lanacHotel, hotel } by new { lanacHotel.Id, lanacHotel.Naziv, lanacHotel.GodinaOsnivanja } into g
                          select new LanacDTO
                          {
                              Id = g.Key.Id,
                              Naziv = g.Key.Naziv,
                              UkupanBrojSoba = g.Sum(x => x.hotel.BrojSoba)
                          }).ToList().OrderBy(a => a.UkupanBrojSoba);



            var lanci = lanci1.Where(a => a.UkupanBrojSoba > granica).OrderBy(a => a.UkupanBrojSoba);

            return lanci;

        }
    }
}