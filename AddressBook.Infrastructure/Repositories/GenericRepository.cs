using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AddressBook.Application.Interfaces;
using AddressBook.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly AddressBookDBContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AddressBookDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);
        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) => await _dbSet.Where(predicate).ToListAsync();
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();
        public virtual async Task<TEntity?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public virtual void Remove(TEntity entity) => _dbSet.Remove(entity);
        public virtual void Update(TEntity entity) => _dbSet.Update(entity);
    }
}
