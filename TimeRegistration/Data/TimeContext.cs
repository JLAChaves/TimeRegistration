using Microsoft.EntityFrameworkCore;
using TimeRegistration.Models;

namespace TimeRegistration.Data
{
    public class TimeContext : DbContext
    {
        public TimeContext(DbContextOptions<TimeContext> options) : base(options)
        {
        }

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<TimeLog> TimesLogs { get; set; }
    }
}
