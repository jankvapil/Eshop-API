using System.Collections.Generic;
using Eshop.GraphQL.Common;
using Eshop.GraphQL.Data;

namespace Eshop.GraphQL.Users
{
    public class UserPayloadBase : Payload
    {
        protected UserPayloadBase(User user)
        {
            User = user;
        }

        protected UserPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public User? User { get; }
    }
}