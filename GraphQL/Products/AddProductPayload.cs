using System.Collections.Generic;
using Eshop.GraphQL.Common;
using Eshop.GraphQL.Data;

namespace Eshop.GraphQL.Products
{
    public class AddProductPayload : ProductPayloadBase
    {
        public AddProductPayload(Product product)
             : base(product)
         {
         }

         public AddProductPayload(IReadOnlyList<UserError> errors)
             : base(errors)
         {
         }
    }
}