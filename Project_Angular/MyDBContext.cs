using Microsoft.EntityFrameworkCore;

namespace Project_Angular
{
    public class MyDBContext : DbContext
    {
        public MyDBContext()
        { }

        public MyDBContext(DbContextOptions<MyDBContext> options)
            : base(options)
        {
        }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Service> Service { get; set; }
    }

}
