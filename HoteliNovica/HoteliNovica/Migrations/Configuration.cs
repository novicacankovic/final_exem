namespace HoteliNovica.Migrations
{
    using HoteliNovica.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HoteliNovica.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HoteliNovica.Models.ApplicationDbContext context)
        {
            context.LanacHotel.AddOrUpdate(x => x.Id,
               new LanacHotel() { Id = 1, Naziv = "Hilton Worldwide", GodinaOsnivanja = 1919 },
               new LanacHotel() { Id = 2, Naziv = "Marriott International", GodinaOsnivanja = 1927 },
               new LanacHotel() { Id = 3, Naziv = "Kempinski ", GodinaOsnivanja = 1897 }
           );

            context.Hotel.AddOrUpdate(x => x.Id,
                new Hotel() { Id = 1, Naziv = " Sheraton Novi Sad", GodinaOtvaranja = 2018, BrojZaposlenih = 70, BrojSoba = 150, LanacHotelId = 2 },
                new Hotel() { Id = 2, Naziv = " Hilton Belgrade", GodinaOtvaranja = 2017, BrojZaposlenih = 100, BrojSoba = 242, LanacHotelId = 1 },
                new Hotel() { Id = 3, Naziv = " Palais Hansen", GodinaOtvaranja = 2013, BrojZaposlenih = 80, BrojSoba = 152, LanacHotelId = 3 },
                new Hotel() { Id = 4, Naziv = " Budapest Marriott", GodinaOtvaranja = 1994, BrojZaposlenih = 130, BrojSoba = 364, LanacHotelId = 2 },
                new Hotel() { Id = 5, Naziv = " Hilton Berlin", GodinaOtvaranja = 1991, BrojZaposlenih = 200, BrojSoba = 601, LanacHotelId = 1 }
            );
        }
    }
}
