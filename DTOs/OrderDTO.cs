using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DTOs
{
    public record OrderDTO
    (
        [Required]
        int OrderId,
        [Required]
        DateOnly OrderDate,
        [Required]
        DateOnly EventDate,
        [Required]
        int FinalPrice,
        [Required]
        int UserId,
        [Required]
        string UserFirstName,
        [Required]
        string UserLasttName,
        string Note,
        [Required]
        List<DressDTO> OrderItems
    );
}
