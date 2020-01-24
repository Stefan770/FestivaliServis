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
    public class FestivaliController : ApiController
    {
        IFestivalRepo _repository { get; set; }

        public FestivaliController(IFestivalRepo repo)
        {
            _repository = repo;
        }

        public IEnumerable<Festival> Get()
        {
            return _repository.GetAll();
        }


        [ResponseType(typeof(Festival))]
        public IHttpActionResult Get(int id)
        {
            var nekretnina = _repository.GetById(id);
            if (nekretnina == null)
            {
                return NotFound();
            }
            return Ok(nekretnina);
        }

        [Authorize]
        [Route("api/festivali/pretraga")]
        public IEnumerable<Festival> PostFiltered(FestivalFilter filter)
        {
            return _repository.GetByYear(filter);
        }

        [Authorize]
        [ResponseType(typeof(Festival))]
        public IHttpActionResult Post(Festival nekretnina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(nekretnina);
            return CreatedAtRoute("DefaultApi", new { id = nekretnina.Id }, nekretnina);
        }

        [Authorize]
        [ResponseType(typeof(Festival))]
        public IHttpActionResult Put(int id, Festival nekretnina)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nekretnina.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(nekretnina);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(nekretnina);
        }

        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            var nekretnina = _repository.GetById(id);
            if (nekretnina == null)
            {
                return NotFound();
            }

            _repository.Delete(nekretnina);
            return Ok();
        }
    }
}
