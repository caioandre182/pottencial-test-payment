using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Tech.Test.Payment.Application.DTO;
using Tech.Test.Payment.Domain.Entities;

namespace Tech.Test.Payment.Tests.Configuration
{
    public class AutoMapperConfiguration
    {
        public static IMapper GetConfiguration()
        {
            var autoMapperConfig= new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Sale, SaleDTO>()
                    .ReverseMap();
            }
            );

            return autoMapperConfig.CreateMapper();
        }
    }
}