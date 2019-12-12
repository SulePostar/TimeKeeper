using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TimeKeeper.Domain;

namespace TimeKeeper.DAL
{
    public class TimeContext : DbContext
    {
        private string _type;
        private string _conStr;
        public TimeContext() : base()
        {
            _type = "PGS";
            _conStr = "User ID=postgres; Password=osmanaga; Server=localhost; Port=5432; Database=tracker; Integrated Security=true; Pooling=true;";
        }
        public TimeContext(DbContextOptions<TimeContext> options) : base(options)
        {
            var x = options;
        }
        public TimeContext(string type, string conStr)
        {
            _type = type;
            _conStr = conStr;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Day> Calendar { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Employee> People { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (_conStr != null)
            {
                if(_type == "SQL")
                {
                    builder.UseSqlServer(_conStr);
                }
                else
                {
                    builder.UseNpgsql(_conStr);
                }
            }
            builder.UseLazyLoadingProxies(true);
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Customer>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<Day>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<Detail>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<Employee>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<Member>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<Project>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<Role>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<Team>().HasQueryFilter(x => !x.Deleted);

            builder.Entity<Customer>().OwnsOne(x => x.Address);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken token = default)
        {
            foreach (var entry in ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted && x.Entity is BaseClass))
            {
                entry.State = EntityState.Modified;
                entry.CurrentValues["Deleted"] = true;
            }
            return await base.SaveChangesAsync(token);
        }
    }
}
