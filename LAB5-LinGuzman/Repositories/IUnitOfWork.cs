using System;
using System.Threading.Tasks;

namespace LAB5_LinGuzman.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task SaveAsync();
    }
}