using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class User
    {
        [EmailAddress, Required]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
        public string Password { get; set; }
    }
   
}
