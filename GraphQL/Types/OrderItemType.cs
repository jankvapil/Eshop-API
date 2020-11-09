using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Eshop.GraphQL.Data;
using Eshop.GraphQL.DataLoader;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Resolvers;

namespace Eshop.GraphQL.Types
{
    public class OrderItemType : ObjectType<OrderItem>
    {
        protected override void Configure(IObjectTypeDescriptor<OrderItem> descriptor)
        {
            descriptor
                .Field(t => t.Product)
                .ResolveWith<OrderItemResolvers>(t => t.GetProductAsync(default!, default!, default!, default))
                .UseDbContext<ApplicationDbContext>()
                .Name("product");
        }

        private class OrderItemResolvers
        {
            public async Task<IEnumerable<Product>> GetProductAsync(
                OrderItem OrderItem,
                [ScopedService] ApplicationDbContext dbContext,
                ProductByIdDataLoader productById,
                CancellationToken cancellationToken)
            {
                int[] Ids = await dbContext.Products
                    .Where(p => p.Id == OrderItem.ProductId)
                    .Select(col => col.Id)
                    .ToArrayAsync();

                return await productById.LoadAsync(Ids, cancellationToken);
            }
        }
    }
}