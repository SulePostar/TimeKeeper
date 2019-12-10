using System;
using System.Threading.Tasks;
using TimeKeeper.Domain;

namespace TimeKeeper.DAL
{
    public class UnitOfWork: IDisposable
    {
        protected TimeContext _context;
        public UnitOfWork(TimeContext context)
        {
            _context = context;
        }

        private IRepository<Customer> _customers;
        private IRepository<Day> _calendar;
        private IRepository<Detail> _details;
        private IRepository<Employee> _people;
        private IRepository<Member> _members;
        private IRepository<Project> _projects;
        private IRepository<Role> _roles;
        private IRepository<Team> _teams;
        private IRepository<User> _users;

        public TimeContext Context { get { return _context; } }

        public IRepository<Customer> Customers => _customers ?? (_customers = new Repository<Customer>(_context));
        public IRepository<Day> Calendar => _calendar ?? (_calendar = new Repository<Day>(_context));
        public IRepository<Detail> Details => _details ?? (_details = new Repository<Detail>(_context));
        public IRepository<Employee> People => _people ?? (_people = new Repository<Employee>(_context));
        public IRepository<Member> Members => _members ?? (_members = new Repository<Member>(_context));
        public IRepository<Project> Projects => _projects ?? (_projects = new Repository<Project>(_context));
        public IRepository<Role> Roles => _roles ?? (_roles = new Repository<Role>(_context));
        public IRepository<Team> Teams => _teams ?? (_teams = new Repository<Team>(_context));
        public IRepository<User> Users => _users ?? (_users = new Repository<User>(_context));

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
