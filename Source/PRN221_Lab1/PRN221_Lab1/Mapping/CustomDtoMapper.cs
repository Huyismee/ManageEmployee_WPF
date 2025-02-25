using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PRN221_Lab1.DTO;
using PRN221_Lab1.Models;

namespace PRN221_Lab1.Mapping
{
    internal class CustomDtoMapper : Profile
    {
        public CustomDtoMapper()
        {
            CreateMap<Order, OrdersDto>().ReverseMap();
        }
    }
}
