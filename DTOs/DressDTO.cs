using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record DressDTO
    (
        [Required]
        int Id,
        [Required]
        int ModelName,
        [Required]
        string Size,
        [Required]
        int Price,
        string Note,
        [Required]
        bool IsActive,
        [Required]
        string ModelImgUrl
    );
}
