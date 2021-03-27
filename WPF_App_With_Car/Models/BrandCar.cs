using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_App_With_Car.Models
{
    public class BrandCar
    {
        [Key]
        public string Brand_Name { get; set; }
        public string Description { get; set; }
        public byte[] Logotype { get; set; }
        public string Type_Image { get; set; }
    }
}
