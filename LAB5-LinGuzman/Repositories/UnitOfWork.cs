using LAB5_LinGuzman.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LAB5_LinGuzman.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IntitutobdContext _context;
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWork(IntitutobdContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (!_repositories.ContainsKey(typeof(TEntity)))
            {
                _repositories[typeof(TEntity)] = new Repository<TEntity>(_context);
            }
            return (IRepository<TEntity>)_repositories[typeof(TEntity)];
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}