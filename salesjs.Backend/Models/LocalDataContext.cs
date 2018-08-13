

namespace salesjs.Backend.Models
{
    using Domain.Models;

    //esta clase se usa para habilitar las migraciones
    //automaticas del EF
    public class LocalDataContext: DataContext
    {
        public System.Data.Entity.DbSet<salesjs.Common.Models.Product> Products { get; set; }
    }
}