using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Eshop.GraphQL.Data;
using Eshop.GraphQL.DataLoader;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace Eshop.GraphQL.Products
{
    [ExtendObjectType(Name = "Query")]
    public class ProductQueries
    {
        [UseApplicationDbContext]
        public Task<List<Product>> GetProducts([ScopedService] ApplicationDbContext context) =>
            context.Products.ToListAsync();


        public Task<Product> GetProductAsync(
            int id,
            ProductByIdDataLoader dataLoader,
            CancellationToken cancellationToken) =>
            dataLoader.LoadAsync(id, cancellationToken);
    }
}