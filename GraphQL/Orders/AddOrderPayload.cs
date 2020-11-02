using System.Collections.Generic;
using Eshop.GraphQL.Common;
using Eshop.GraphQL.Data;

namespace Eshop.GraphQL.Orders
{
    public class AddOrderPayload : OrderPayloadBase
    {
        public AddOrderPayload(Order order)
             : base(order)
         {
         }

         public AddOrderPayload(IReadOnlyList<UserError> errors)
             : base(errors)
         {
         }
    }
}