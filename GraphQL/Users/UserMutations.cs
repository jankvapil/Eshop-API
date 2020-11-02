using System.Threading;
using System.Threading.Tasks;
using Eshop.GraphQL.Common;
using Eshop.GraphQL.Data;
using HotChocolate;
using HotChocolate.Types;

namespace Eshop.GraphQL.Users
{
    [ExtendObjectType(Name = "Mutation")]
    public class UserMutations
    {
      
        [UseApplicationDbContext]
        public async Task<AddUserPayload> AddUserAsync(
            AddUserInput input,
            [ScopedService] ApplicationDbContext context)
        {
            var user = new User
            {
                Name = input.Name,
                Email = input.Email
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return new AddUserPayload(user);
        }
    }
}