using System;
using GraphQL.Types;
using GraphQLASP.Data;
using GraphQLASP.Mutations;
using GraphQLASP.Queries;

namespace GraphQLASP.Schemas
{
    public class DemoSchema : Schema
    {
        public DemoSchema(IServiceProvider sp) : base(sp)
        {
            ApplicationDbContext ctx = sp.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;

            this.Query = new CustomerQuery(ctx);
            this.Mutation = new CustomerMutation(ctx);
        }
    }
}