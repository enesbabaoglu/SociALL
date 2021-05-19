using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServerApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApp.Repositories.Concrete
{
    public class SociAllContext : IdentityDbContext<User,Role,int>
    {
        public SociAllContext(DbContextOptions<SociAllContext> options):base(options)
        {
            
        }
    }
}

