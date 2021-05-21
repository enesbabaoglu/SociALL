using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ServerApp.Entities;

namespace ServerApp.Repositories.Abstract
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}