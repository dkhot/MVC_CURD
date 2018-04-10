using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IRepository<T>
    {


        int Count();

        IQueryable<T> GetAll();

        void Add(T entity);

        void Delete(T entity);

        void Edit(T entity);
        
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
    }
}
