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
    public class CarsController : ApiController
    {
        private CarDbContext db = new CarDbContext();

        // GET: api/Cars
        public IQueryable<Car> GetCars()
        {
            return db.Cars;
        }

        // GET: api/Cars/5
        [ResponseType(typeof(Car))]
        public IHttpActionResult GetCar(int id)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        // PUT: api/Cars/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCar(int id, Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != car.CarId)
            {
                return BadRequest();
            }

            //start
            var org = db.Cars.Include(x => x.CarParts).First(x => x.CarId == car.CarId);
            org.CarModel = car.CarModel;
            org.Type = car.Type;
            org.Price = car.Price;
            org.IsAvailable = car.IsAvailable;
            if (car.CarParts != null && car.CarParts.Count > 0)
            {
                var parts = car.CarParts.ToArray();
                for (var i = 0; i < parts.Length; i++)
                {
                    var temp = org.CarParts.FirstOrDefault(y => y.CarPartsId == parts[i].CarPartsId);
                    if (temp != null)
                    {
                        temp.PartsName = parts[i].PartsName;
                        temp.Stock = parts[i].Stock;
                        temp.Price = parts[i].Price;
                    }
                    else
                    {
                        org.CarParts.Add(parts[i]);
                    }
                }
                parts = org.CarParts.ToArray();
                for (var i = 0; i < parts.Length; i++)
                {
                    var tmp = car.CarParts.FirstOrDefault(j => j.CarPartsId == parts[i].CarPartsId);
                    if (tmp == null)
                        db.Entry(parts[i]).State = EntityState.Deleted;
                }
            }

            //db.Entry(car).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
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

        // POST: api/Cars
        [ResponseType(typeof(Car))]
        public IHttpActionResult PostCar(Car car)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            db.Cars.Add(car);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = car.CarId }, car);
        }

        // DELETE: api/Cars/5
        [ResponseType(typeof(Car))]
        public IHttpActionResult DeleteCar(int id)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }

            db.Cars.Remove(car);
            db.SaveChanges();

            return Ok(car);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarExists(int id)
        {
            return db.Cars.Count(e => e.CarId == id) > 0;
        }
        // GET: api/Cars
        [Route("custom/carList")]
        public IQueryable<Car> GetCarsWithParts()
        {
            return db.Cars.Include(x=>x.CarParts);
        }
        //custom {id}
        [Route("custom/Cars/{id}")]
        public IHttpActionResult GetDeptWithRelated(int id)
        {
            return Ok(db.Cars
                    .Include(x => x.CarParts)
                    .FirstOrDefault(x => x.CarId == id));
        }
    }
}