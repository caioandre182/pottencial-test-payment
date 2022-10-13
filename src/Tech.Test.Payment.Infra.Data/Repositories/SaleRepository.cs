using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tech.Test.Payment.Domain.Entities;
using Tech.Test.Payment.Domain.Repositories;
using Tech.Test.Payment.Infra.Data.Context;
using Tech.Test.Payment.Domain.Validations;

namespace Tech.Test.Payment.Infra.Data.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DataContext _context;

        public SaleRepository(DataContext context)
        {
            _context = context;
        }

        //Create
        public async Task<Sale> CreateAsync(Sale sale)
        {
            _context.Add(sale);
            await _context.SaveChangesAsync();
            return sale;
        }

        //Edit
        public async Task<Sale> UpdateAsync(Sale sale)
        {
                 _context.Update(sale);
                await _context.SaveChangesAsync();
                return sale;
        }

        //Get
        public async Task<Sale> GetByIdAsync(int id)
        {
            var sale = await _context.Sales.FirstOrDefaultAsync(x => x.Id == id);
            if (sale == null)
                return null;
            return sale;
        }

        public async Task<Sale> GetStatus(int id)
        {
            return await _context.Sales.FindAsync(id);
        }
    }
}