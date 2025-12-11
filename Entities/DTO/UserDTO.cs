using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public record UserDTO
    (
        int UserId,
        [EmailAddress,Required]
        string UserName,
        string FirstName ,
        string LastName ,
        [Required]
        string Password 
    );  
}
