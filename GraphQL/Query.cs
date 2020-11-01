using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using HotChocolate;
using System.Threading;
using System.Threading.Tasks;
using Eshop.GraphQL.Data;
using Eshop.GraphQL.DataLoader;

namespace Eshop.GraphQL
{
    public class Query
    {
        [UseApplicationDbContext]
        public Task<List<User>> GetUsers([ScopedService] ApplicationDbContext context) =>
            context.Users.ToListAsync();

        public Task<Order> GetOrderAsync(
            int id,
            OrderByIdDataLoader dataLoader,
            CancellationToken cancellationToken) =>
            dataLoader.LoadAsync(id, cancellationToken);
    
    }
}