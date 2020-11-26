using GraphQL;
using GraphQL.Types;
using GraphQLASP.Data;
using GraphQLASP.Models;
using GraphQLASP.Types;

namespace GraphQLASP.Mutations
{
    public class CustomerMutation : ObjectGraphType
    {
        private readonly ApplicationDbContext _db;

        public CustomerMutation(ApplicationDbContext db)
        {
            this._db = db;

            this.Field<CustomerGraphType>("addCustomer",
                arguments: new QueryArguments(
                    new QueryArgument<StringGraphType> {Name = "name"}
                ),
                resolve: ctx =>
                {
                    var name = ctx.GetArgument<string>("name");
                    var customer = new Customer {FirstName = name};

                    this._db.Customers.Add(customer);
                    this._db.SaveChanges();

                    return customer;
                });
        }
    }
}