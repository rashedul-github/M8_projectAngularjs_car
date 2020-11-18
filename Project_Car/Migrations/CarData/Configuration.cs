namespace Project_Car.Migrations.CarData
{
    using Project_Car.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Project_Car.Models.CarDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\CarData";
        }

        protected override void Seed(Project_Car.Models.CarDbContext db)
        {
            Car c = new Car { CarModel = "VVI-78", Type = CarType.Micro, LunchDate = DateTime.Parse("01/01/2020"), Price = 250000.00M, IsAvailable = true };
            c.CarParts.Add(new CarParts { PartsName = "tyre", Stock = 22, Price = 2500.00M,CarId=1 });
            c.CarParts.Add(new CarParts { PartsName = "Breack", Stock = 33, Price = 5500.00M,CarId=1 });
            db.Cars.Add(c);
            Car c1 = new Car { CarModel = "GGT-99", Type = CarType.Coupe, LunchDate = DateTime.Parse("01/01/2018"), Price = 350000.00M, IsAvailable = true };
            c1.CarParts.Add(new CarParts { PartsName = "Glass", Stock = 15, Price = 5600.00M,CarId=2 });
            c1.CarParts.Add(new CarParts { PartsName = "Sheet", Stock = 44, Price = 11000.00M,CarId=2 });
            db.Cars.Add(c1);
            db.SaveChanges();
        }
    }
}
