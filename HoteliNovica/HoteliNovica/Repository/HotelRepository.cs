using HoteliNovica.Interfaces;
using HoteliNovica.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace HoteliNovica.Repository
{
    public class HotelRepository : IDisposable, IHotelRepository
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


        public IEnumerable<Hotel> GetAll()
        {
            return db.Hotel.Include(a => a.LanacHotel).OrderBy(a => a.GodinaOtvaranja);
        }

        public Hotel GetById(int id)
        {
            return db.Hotel.Include(a => a.LanacHotel).FirstOrDefault(a => a.Id == id);
        }

        public void Add(Hotel hotel)
        {
            db.Hotel.Add(hotel);
            db.SaveChanges();
        }

        public void Update(Hotel hotel)
        {
            db.Entry(hotel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(Hotel hotel)
        {
            db.Hotel.Remove(hotel);
            db.SaveChanges();
        }
    }
}