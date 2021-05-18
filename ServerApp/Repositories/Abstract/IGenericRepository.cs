using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotNet5WebApiExample.Repositories.Abstract
{
    public interface IGenericRepository<T>
    {
        T Get(Expression<Func<T, bool>> predicate);

        List<T> GetAll(Expression<Func<T, bool>> predicate);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
