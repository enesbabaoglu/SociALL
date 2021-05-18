using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DotNet5WebApiExample.Repositories.Abstract;
using DotNet5WebApiExample.Repositories.Concrete;
using ServerApp.Entities;

namespace ServerApp.Repositories.Abstract
{
    public interface IUserRepository : IGenericRepository<User>
    {
        // new List<User> GetAll(Expression<Func<User, bool>> predicate);
    }
}