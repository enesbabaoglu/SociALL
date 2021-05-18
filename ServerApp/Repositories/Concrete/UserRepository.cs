using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DotNet5WebApiExample.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;
using ServerApp.Entities;
using ServerApp.Repositories.Abstract;

namespace ServerApp.Repositories.Concrete
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        SociAllContext _sociAllContext;
        DbSet<User> _dbSet;
        public UserRepository(SociAllContext sociAllContext) : base(sociAllContext)
        {
              _sociAllContext = sociAllContext;
            _dbSet = _sociAllContext.Set<User>();
        }

        // public new virtual List<User> GetAll(Expression<Func<User, bool>> predicate){

        //     return _dbSet.Where(predicate).Include(i => i.Images).ToList();
            
        // }
    }
}