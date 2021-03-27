using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_App_With_Car.Models.Contexts
{

    public class Cars_DB : DbContext
    {
        public Cars_DB() : base("DataContext")
        { }
        public DbSet<Cars> Cars { get; set; }
        public DbSet<BrandCar> BrandCar { get; set; }

    }
}
