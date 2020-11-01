using System.Threading.Tasks;
using Eshop.GraphQL.Data;
using HotChocolate;

namespace Eshop.GraphQL
{
    public class Mutation
    {
      
        [UseApplicationDbContext]
        public async Task<AddUserPayload> AddUserAsync(
            AddUserInput input,
            [Service] ApplicationDbContext context)
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