using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Eshop.GraphQL.Data;
using Eshop.GraphQL.DataLoader;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace Eshop.GraphQL.Users
{
    [ExtendObjectType(Name = "Query")]
    public class UserQueries
    {
        [UseApplicationDbContext]
        public Task<List<User>> GetUsers([ScopedService] ApplicationDbContext context) =>
            context.Users.ToListAsync();


        public Task<User> GetUserAsync(
            [ID(nameof(User))]System.Guid id,
            UserByIdDataLoader dataLoader,
            CancellationToken cancellationToken) =>
            dataLoader.LoadAsync(id, cancellationToken);
            

        // public Task<User> GetUserAsync(
        //     int id,
        //     UserByIdDataLoader dataLoader,
        //     CancellationToken cancellationToken) =>
        //     dataLoader.LoadAsync(id, cancellationToken);

        // public Task<Order> GetOrderAsync(
        //     int id,
        //     OrderByIdDataLoader dataLoader,
        //     CancellationToken cancellationToken) =>
        //     dataLoader.LoadAsync(id, cancellationToken);
    
    }
}