using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project_Car.Models
{
    public enum CarType { Pickup = 1, Van, Minivan, Truck, Minitruck, Bigtruck, Coupe, Micro, Sedan, Sub, Supercar, Roadster }

    public class Car
    {
        public Car()
        {
            this.CarParts = new List<CarParts>();
        }
        [Display(Name = "Car Id")]
        public int CarId { get; set; }
        [Required(ErrorMessage = "Car model is required,"), StringLength(40), Display(Name = "Car Model")]
        public string CarModel { get; set; }
        [EnumDataType(typeof(CarType)), Display(Name = "Car Type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CarType? Type { get; set; }
        [Required(ErrorMessage = "Car price is required."), Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        [Display(Name = "Car Price")]
        public decimal Price { get; set; }
        [Required, Display(Name = "Car Is Available?")]
        public bool IsAvailable { get; set; }
        [Required(ErrorMessage = "Date is required."), Display(Name = "Lunch Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LunchDate { get; set; }
        //
        public virtual ICollection<CarParts> CarParts { get; set; }
    }
    public class CarParts
    {
        [Display(Name = "Parts Id")]
        public int CarPartsId { get; set; }
        [Required(ErrorMessage = "Parts name is required."), StringLength(40)]
        [Display(Name = "Parts Name")]
        public string PartsName { get; set; }
        [Required(ErrorMessage = "Stock is required.")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "Parts price is required."), Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        [Display(Name = "Parts Price")]
        public decimal Price { get; set; }
        //
        [Required, Display(Name = "Car Id")]
        public int CarId { get; set; }
        [ForeignKey("CarId")]
        public virtual Car Car { get; set; }

    }
    public class CarDbContext : DbContext
    {
        public CarDbContext()
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarParts> CarParts { get; set; }
    }
}