using Microsoft.EntityFrameworkCore;


namespace Agenda_API.Models
{
    public class AgendaDbContext : DbContext
    {
        public AgendaDbContext(DbContextOptions<AgendaDbContext> options)
            :base(options)
        {
        }

        public virtual DbSet<Agenda> Agenda { get; set; }
    }
}
