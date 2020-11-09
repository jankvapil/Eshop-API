using System.Collections.Generic;
using Eshop.GraphQL.Common;
using Eshop.GraphQL.Data;

namespace Eshop.GraphQL.Products
{
    public class ProductPayloadBase : Payload
    {
        protected ProductPayloadBase(Product product)
        {
            Product = product;
        }

        protected ProductPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Product? Product { get; init; }
    }
}