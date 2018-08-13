using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace salesjs.Domain.Models
{
    public class DataContext: DbContext
    {
        public DataContext():base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<salesjs.Common.Models.Product> Products { get; set; }
    }
}
