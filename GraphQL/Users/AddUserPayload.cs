using System.Collections.Generic;
using Eshop.GraphQL.Common;
using Eshop.GraphQL.Data;

namespace Eshop.GraphQL.Users
{
    public class AddUserPayload : UserPayloadBase
    {
        public AddUserPayload(User user)
             : base(user)
         {
         }

         public AddUserPayload(IReadOnlyList<UserError> errors)
             : base(errors)
         {
         }
    }
}