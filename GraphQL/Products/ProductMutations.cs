using System.Threading;
using System.Threading.Tasks;
using Eshop.GraphQL.Common;
using Eshop.GraphQL.Data;
using HotChocolate;
using HotChocolate.Types;

namespace Eshop.GraphQL.Products
{
    [ExtendObjectType(Name = "Mutation")]
    public class ProductMutations
    {
      
        [UseApplicationDbContext]
        public async Task<AddProductPayload> AddProductAsync(
            AddProductInput input,
            [ScopedService] ApplicationDbContext context)
        {
            // create new Product
            var product = new Product
            {
                Name = input.Name,
                Price = input.Price
            };

            // add to db-context
            context.Products.Add(product);

            // save changes
            await context.SaveChangesAsync();

            return new AddProductPayload(product);
        }
    }
}