using Microsoft.EntityFrameworkCore;

namespace API_RESTful.Models
{
    public class MyDBcontext : DbContext
    {
        // Constructor
        public MyDBcontext(DbContextOptions<MyDBcontext> options)
            : base(options)
        {

        }


        // Tablas a Mapear en DB:
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
    }

}
