using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Eshop.GraphQL.Data;
using Eshop.GraphQL.DataLoader;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;

using GraphQL;
using System.Linq;
using System.Collections.Generic;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Eshop.GraphQL.Users
{
    [ExtendObjectType(Name = "Query")]
    public class UserQueries
    {
        // TODO: ADD AUTHORIZATION!
                public string Unauthorized()
        {
            return "unauthorized";
        }

        [Authorize]
        public List<string> Authorized([Service] IHttpContextAccessor contextAccessor)
        {
            return contextAccessor.HttpContext.User.Claims.Select(x => $"{x.Type} : {x.Value}").ToList();
        }

        [Authorize(Roles = new[] {"admin"})]
        public string AdminOnly()
        {
            return "admin only";
        }

        [Authorize]
        public List<string> AuthorizedBetterWay([GlobalState("currentUser")] CurrentUser user)
        {
            return user.Claims;
        }


    [   Authorize(Roles = new[] {"leader"})]
        public List<string> AuthorizedLeader([GlobalState("currentUser")] CurrentUser user)
        {
            return user.Claims;
        }

        [Authorize(Roles = new[] {"dev"})]
        public List<string> AuthorizedDev([GlobalState("currentUser")] CurrentUser user)
        {
            return user.Claims;
        }

        [Authorize(Policy = "DevDepartment")]
        public List<string> AuthorizedDevDepartment([GlobalState("currentUser")] CurrentUser user)
        {
            return user.Claims;
        }

        //////////////////////////////////
        
        [UseApplicationDbContext]
        public Task<List<User>> GetUsers([ScopedService] ApplicationDbContext context) =>
            context.Users.ToListAsync();


        public Task<User> GetUserAsync(
            int id,
            UserByIdDataLoader dataLoader,
            CancellationToken cancellationToken) =>
            dataLoader.LoadAsync(id, cancellationToken);
    }
}