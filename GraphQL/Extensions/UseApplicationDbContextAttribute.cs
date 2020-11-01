using System.Reflection;
using Eshop.GraphQL.Data;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;

namespace Eshop.GraphQL
{
    public class UseApplicationDbContextAttribute : ObjectFieldDescriptorAttribute
    {
        public override void OnConfigure(
            IDescriptorContext context,
            IObjectFieldDescriptor descriptor,
            MemberInfo member)
        {
            descriptor.UseDbContext<ApplicationDbContext>();
        }
    }
}