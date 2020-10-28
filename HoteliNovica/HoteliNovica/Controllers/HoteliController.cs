using HoteliNovica.Interfaces;
using HoteliNovica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HoteliNovica.Controllers
{
    public class HoteliController : ApiController
    {
        IHotelRepository _repository { get; set; }

        public HoteliController(IHotelRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Hotel> Get()
        {
            return _repository.GetAll().OrderBy(a => a.GodinaOtvaranja);
        }

        public IHttpActionResult Get(int id)
        {
            var hotel = _repository.GetById(id);
            if (hotel == null)
            {
                return NotFound();
            }
            else
                return Ok(hotel);
        }

        public IHttpActionResult Post(Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(hotel);
            hotel = _repository.GetById(hotel.Id);
            return CreatedAtRoute("DefaultApi", new { id = hotel.Id }, hotel);
        }
        [Authorize]
        public IHttpActionResult Put(int id, Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hotel.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(hotel);

            }
            catch
            {
                throw;
            }

            return Ok(hotel);
        }

        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            var hotel = _repository.GetById(id);
            if (hotel == null)
            {
                return NotFound();
            }
            else
                _repository.Delete(hotel);
            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("api/kapacitet")]
        public IEnumerable<Hotel> SearchPost([FromBody] HotelPretraga hotelPretraga)
        {
            return _repository.GetAll().Where(x => x.BrojSoba > hotelPretraga.Najmanje && x.BrojSoba < hotelPretraga.Najvise).OrderByDescending(a => a.BrojSoba);
        }

        [HttpGet]
        public IEnumerable<Hotel> Search(int zaposleni)
        {
            return _repository.GetAll().Where(a => a.BrojZaposlenih >= zaposleni).OrderBy(a => a.BrojZaposlenih);
        }

    }
}
