using FestivaliServis.Models;
using FestivaliServis.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace FestivaliServis.Controllers
{
    public class MestaController : ApiController
    {
        IMestoRepo _repository { get; set; }

        public MestaController(IMestoRepo repo)
        {
            _repository = repo;
        }

        public IEnumerable<Mesto> Get()
        {
            return _repository.GetAll();
        }

        public IEnumerable<Mesto> GetByZip(int kod)
        {
            return _repository.GetByZip(kod);
        }

        [ResponseType(typeof(Mesto))]
        public IHttpActionResult Get(int id)
        {
            var state = _repository.GetById(id);
            if (state == null)
            {
                return NotFound();
            }
            return Ok(state);
        }
    }
}
