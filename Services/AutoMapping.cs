using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.DTO;
using Repositories;
namespace Services
{
    public class AutoMapping : Profile
    {
        public AutoMapping() 
        {
            CreateMap<User,UserDTO>();
            CreateMap<User,UserLoginDTO>();
            CreateMap<Category,CategoryDTO>();
            CreateMap<Order, OrderDTO>();//.ForMember(dest=> dest.OrderItems,
                                             //       opts=>opts.MapFrom(src=>(src.OrderItems.qua, src.OrderItems.)));
            CreateMap<Product, ProductDTO>().ForMember(dest => dest.CategoryName,
                                                       opts=>opts.MapFrom(src=>src.Category.CategoryName));
        }
    }
}
