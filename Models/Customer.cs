using System.ComponentModel.DataAnnotations;

namespace GraphQLASP.Models
{
    public class Customer
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        // public virtual string LastName { get; set; }
        // public virtual string Contact { get; set; }
        // public virtual string Email { get; set; }
    }
}