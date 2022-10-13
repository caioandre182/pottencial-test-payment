using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tech.Test.Payment.Domain.Entities;


namespace Tech.Test.Payment.Domain.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale> GetByIdAsync(int id);
        Task<Sale> CreateAsync(Sale sale);
        Task<Sale> UpdateAsync(Sale sale);
        Task<Sale> GetStatus(int id);
    }
}