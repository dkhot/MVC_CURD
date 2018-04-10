using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly EmployeeContext _context;

        protected RepositoryBase()
            : this(IocContainer.Instance.Get<IUnitofWork>())
        {

        }
        private RepositoryBase(IUnitofWork unitOfWork)
        {
            _context = (EmployeeContext)unitOfWork;
        }



        public int Count()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Edit(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }


        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }
    }

}
