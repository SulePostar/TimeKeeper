using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TimeKeeper.Domain;

namespace TimeKeeper.DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected TimeContext _context;
        protected DbSet<T> dbSet;

        public Repository(TimeContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        public async Task<IList<T>> Get()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<IList<T>> Get(Expression<Func<T, bool>> where)
        {
            return await dbSet.Where(where).ToListAsync();
        }

        public async Task<T> Get(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task Insert(T newEnt)
        {
            await newEnt.Build(_context);
            dbSet.Add(newEnt);
        }

        public async Task Update(T newEnt, int id)
        {
            T oldEnt = await Get(id);
            if (oldEnt != null)
            {
                await newEnt.Build(_context);
                if (typeof(T) == typeof(User)) (newEnt as User).Password = (oldEnt as User).Password;
                _context.Entry(oldEnt).CurrentValues.SetValues(newEnt);
                oldEnt.Update(newEnt);
            }
        }

        public void Delete(T entity) => dbSet.Remove(entity);

        public async Task Delete(int id)
        {
            T entity = await Get(id);            
            if (entity != null && entity.CanDelete()) Delete(entity);
        }
    }
}
