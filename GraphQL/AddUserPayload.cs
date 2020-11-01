using Eshop.GraphQL.Data;

namespace Eshop.GraphQL
{
    public class AddUserPayload
    {
        public AddUserPayload(User user)
        {
            User = user;
        }

        public User User { get; }
    }
}