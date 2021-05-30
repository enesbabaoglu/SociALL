using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using ServerApp.Repositories.Abstract;

namespace ServerApp.Helpers
{
    public class LastActiveActionFilter : IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();
            
            var id = (int.Parse)(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value); 

            var repository = (IUserRepository)resultContext.HttpContext.RequestServices.GetService(typeof(IUserRepository));

            var user = repository.Get(x=> x.Id == id);

            user.LastActive = DateTime.Now;

            repository.Update(user);
        }
    }
}