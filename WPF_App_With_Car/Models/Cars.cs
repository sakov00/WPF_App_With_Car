using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_App_With_Car.Models
{

    public class Cars
    {
        [Key]
        public string Car_Name { get; set; }
        public string Brand_Name { get; set; }
        public string Type_Fuel { get; set; }
        public float Price { get; set; }
        public DateTime Date_Appearance { get; set; }
        public string Class_Car { get; set; }
        public int Capacity_People { get; set; }
        public bool Availability { get; set; }

    }
}
