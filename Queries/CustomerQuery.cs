using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using GraphQLASP.Data;
using GraphQLASP.Models;
using GraphQLASP.Types;
using Microsoft.EntityFrameworkCore;

namespace GraphQLASP.Queries
{
    public class CustomerQuery : ObjectGraphType
    {
        private readonly ApplicationDbContext _db;

        public CustomerQuery(ApplicationDbContext db)
        {
            this._db = db;
            this.Name = "Query";

            this.Field<ListGraphType<CustomerGraphType>>("customers", "Returns a list of Customer",
                resolve: ctx => { return this._db.Customers.ToList(); });

            this.Field<CustomerGraphType>("customer", "Returns a single customer", new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> {Name = "id", Description = "Customer Id"}
                ),
                ctx => this._db.Customers.Single(x => x.Id == ctx.Arguments["id"].GetPropertyValue<int>()));

            this.Field<ListGraphType<CustomerGraphType>>("customersWhere", "Where query on customer",
                new QueryArguments(
                    new QueryArgument<CustomerWhereType> {Name = "where", Description = "Where lambda"}
                ),
                ctx =>
                {
                    // var arg = ctx.Arguments.First();
                    // foreach (var arg in ctx.Arguments)
                    // {
                    //     
                    // }

                    // var arg = ctx.GetArgument<CustomerWhereType>("where");
                    //
                    // var f = arg.Fields.First();
                    // var val = arg.GetPropertyValue(f.Name);

                    // arg.GetPropertyValue(f)

                    var args = ctx.GetArgument<Dictionary<string, Object>>("where");
                    var str = "SELECT * FROM Customers";
                    var first = true;
                    foreach (var arg in args)
                    {
                        if (first)
                            str += " where";
                        else
                            str += " and";
                        str += $" {arg.Key} = \"{arg.Value}\"";

                        first = false;
                    }
                    

                    return this._db.Customers.FromSqlRaw(str).ToList();

                    // return this._db.Customers
                    // .Where(c => c.GetPropertyValue(arg.Key).ToString() == arg.Value.ToString()).ToList();
                    // .Where(String.Format("CategoryID={0}" & Request.QueryString["id"])).ToList();

                    // return this._db.Customers.Single(x => x.Id == ctx.Arguments["id"].GetPropertyValue<int>());
                });
        }
    }

    public class CustomerWhereType : InputObjectGraphType
    {
        public CustomerWhereType()
        {
            // this.Field(x => x.Id, type: typeof(IdGraphType)).Description("Id desc");
            // this.Field(x => x.FirstName).Description("First name desc");
            this.Field<IntGraphType>("id");
            this.Field<StringGraphType>("firstName");
        }
    }
}