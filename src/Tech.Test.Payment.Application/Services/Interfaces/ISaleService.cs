using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tech.Test.Payment.Application.DTO;
using Tech.Test.Payment.Domain.Entities;

namespace Tech.Test.Payment.Application.Services.Interfaces
{
    public interface ISaleService
    {
        Task<ResultService<SaleDTO>> CreateAsync(SaleDTO saleDTO);
        Task<ResultService<SaleDTO>> GetByIdAsync(int id);
        Task<ResultService> UpdateAsync(SaleDTO saleDTO);
    }
}