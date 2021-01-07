using System.Threading;
using System.Threading.Tasks;
using Eshop.GraphQL.Common;
using Eshop.GraphQL.Data;
using GraphQL;
using HotChocolate;
using HotChocolate.Types;

using System.Linq;
using System.Collections.Generic;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

// using BC = BCrypt.Net.BCrypt;

namespace Eshop.GraphQL.Users
{
    [ExtendObjectType(Name = "Mutation")]
    public class UserMutations
    {

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


        /////////////////////////////////////////
        
        [UseApplicationDbContext]
        public Task<string> GetToken(
            string email, 
            string password, 
            [Service] IIdentityService identityService, 
            [ScopedService] ApplicationDbContext context
        ) => identityService.Authenticate(email, password, context);
            
      
        [UseApplicationDbContext]
        public async Task<AddUserPayload> AddUserAsync(
            AddUserInput input,
            [ScopedService] ApplicationDbContext context)
        {
            // hash password
            // var hashedPass = BC.HashPassword(input.Password);
            
            // create new user
            var user = new User
            {
                Name = input.Name,
                Email = input.Email,
                // Password = hashedPass,
                Password = input.Password,
                Address = input.Address
            };

            // add to db-context
            context.Users.Add(user);

            // save changes
            await context.SaveChangesAsync();

            return new AddUserPayload(user);
        }


        [UseApplicationDbContext]
        public async Task<AddUserOrderPayload> AddUsersOrderAsync(
            AddUserOrderInput input,
            [ScopedService] ApplicationDbContext context)
        {
            // check if orders exists
            if (input.OrderIds.Count == 0)
            {
                return new AddUserOrderPayload(
                    new UserError("No orders assigned.", "NO_ORDERS"));
            }

            // find the existing user by id in db-context
            User user = await context.Users.FindAsync(input.UserId);
            
            foreach (int orderId in input.OrderIds)
            {
                user.UserOrders.Add(new UserOrder
                {
                    OrderId = orderId
                });
            }

            await context.SaveChangesAsync();

            return new AddUserOrderPayload(user);
        }
    }
}