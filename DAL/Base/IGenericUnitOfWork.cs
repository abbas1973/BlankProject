using System;

namespace DAL.Interface
{
    public interface IUnitOfWork<TEntity> : IUnitOfWork, IDisposable where TEntity : class
    {
        IRepository<TEntity> Entities { get; }

    }
}
