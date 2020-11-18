using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Project_Car.Models;

namespace Project_Car.Controllers
{
    public class CarPartsController : ApiController
    {
        private CarDbContext db = new CarDbContext();

        // GET: api/CarParts
        public IQueryable<CarParts> GetCarParts()
        {
            return db.CarParts;
        }

        // GET: api/CarParts/5
        [ResponseType(typeof(CarParts))]
        public IHttpActionResult GetCarParts(int id)
        {
            CarParts carParts = db.CarParts.Find(id);
            if (carParts == null)
            {
                return NotFound();
            }

            return Ok(carParts);
        }

        // PUT: api/CarParts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCarParts(int id, CarParts carParts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != carParts.CarPartsId)
            {
                return BadRequest();
            }

            db.Entry(carParts).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarPartsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CarParts
        [ResponseType(typeof(CarParts))]
        public IHttpActionResult PostCarParts(CarParts carParts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CarParts.Add(carParts);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = carParts.CarPartsId }, carParts);
        }

        // DELETE: api/CarParts/5
        [ResponseType(typeof(CarParts))]
        public IHttpActionResult DeleteCarParts(int id)
        {
            CarParts carParts = db.CarParts.Find(id);
            if (carParts == null)
            {
                return NotFound();
            }

            db.CarParts.Remove(carParts);
            db.SaveChanges();

            return Ok(carParts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarPartsExists(int id)
        {
            return db.CarParts.Count(e => e.CarPartsId == id) > 0;
        }
    }
}