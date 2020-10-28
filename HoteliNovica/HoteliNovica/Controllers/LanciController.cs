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
    public class LanciController : ApiController
    {

        ILanacHotelRepository _repository { get; set; }

        public LanciController(ILanacHotelRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<LanacHotel> Get()
        {
            return _repository.GetAll();
        }

        [Authorize]
        public IHttpActionResult Get(int id)
        {
            var lanac = _repository.GetById(id);
            if (lanac == null)
            {
                return NotFound();
            }
            else
                return Ok(lanac);
        }

        [Authorize]
        [HttpGet]
        [Route("api/tradicija")]
        public IEnumerable<LanacHotel> GetTradition()
        {
            var lanci = _repository.GetAll().OrderBy(x => x.GodinaOsnivanja).Take(2);
            return lanci;
        }

        [HttpGet]
        [Route("api/zaposleni")]
        public IEnumerable<LanacDTO> GetStatistics()
        {
            var lanci = _repository.GetStatistics();
            return lanci;
        }

        [Authorize]
        [HttpPost]
        [Route("api/sobe")]
        public IEnumerable<LanacDTO> SobePretraga(int granice)
        {

            var lanci = _repository.SobaPretrage(granice);
            return lanci;

        }
    }
}
