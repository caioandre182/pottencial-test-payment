using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Tech.Test.Payment.Application.DTO;
using Tech.Test.Payment.Domain.Entities;

namespace Tech.Test.Payment.Application.Mappings
{
    public class DomainToDTOMap : Profile
    {
        public DomainToDTOMap()
        {
            CreateMap<Sale, SaleDTO>();
            CreateMap<Sale, SaleDTO>().ReverseMap();
        }
    }
}