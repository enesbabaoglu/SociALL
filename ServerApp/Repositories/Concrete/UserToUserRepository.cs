using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServerApp.Entities;
using ServerApp.Repositories.Abstract;

namespace ServerApp.Repositories.Concrete
{
    public class UserToUserRepository : GenericRepository<UserToUser>, IUserToUserRepository
    {
        SociAllContext _sociAllContext;
        DbSet<UserToUser> _dbSet;
        public UserToUserRepository(SociAllContext sociAllContext) : base(sociAllContext)
        {
            _sociAllContext = sociAllContext;
            _dbSet = _sociAllContext.Set<UserToUser>();
        }
    }
}