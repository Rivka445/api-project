using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public record ProductDTO
    (
        int ProductId,
        [Required]
        string ProductName,
        string CategoryName,
        string Description,
        [Required]
        double Price,
        string ImgUrl
    );
}
