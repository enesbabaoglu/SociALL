using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ServerApp.Repositories.Abstract
{
    public interface IGenericRepository<T>
    {
        T Get(Expression<Func<T, bool>> predicate);
        T GetWithIncludes(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAllWithIncludes(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        
        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
