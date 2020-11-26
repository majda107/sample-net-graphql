using GraphQL.Types;
using GraphQLASP.Models;

namespace GraphQLASP.Types
{
    public class CustomerGraphType : ObjectGraphType<Customer>
    {
        public CustomerGraphType()
        {
            this.Name = "Customer";

            this.Field(x => x.Id, type: typeof(IdGraphType)).Description("Id desc");
            this.Field(x => x.FirstName).Description("First name desc");
        }
    }
}