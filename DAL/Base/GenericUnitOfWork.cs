using DAL.Interface;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    /// <summary>
    /// کلاس جنریک برای منیجر های اصلی موجودیت ها و منیجر پایه
    /// </summary>
    /// <typeparam name="TEntity">موجودیت</typeparam>
    public class UnitOfWork<TEntity> : UnitOfWork, IUnitOfWork<TEntity> where TEntity : class
    {
        public UnitOfWork(DbContext context) : base(context)
        {
        }

        private Repository<TEntity> _entities;

        public IRepository<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = new Repository<TEntity>(Context);
                return _entities;
            }
        }

    }


}
