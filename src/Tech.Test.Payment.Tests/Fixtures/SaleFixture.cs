using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tech.Test.Payment.Application.DTO;
using Tech.Test.Payment.Domain.Entities;


namespace Tech.Test.Payment.Tests.Fixtures
{
    public class SaleFixture
    {
        public static Sale CreateValidSale()
        {
            return new Sale(
                items : "Carrinho",
                quantityItems : 1,
                idOrder : 1,
                idSeller : 1,
                nameSeller : "Vendedor",
                cpf : "2131241221",
                phone : "2312423135"
            );
        }

        public static SaleDTO CreateValidSaleDTO(bool newId = false)
        {
            return new SaleDTO
            {
                Id = 0,
                IdOrder = 1,
                Items = "Carrinho",
                QuantityItems = 1,
                IdSeller = 1,
                NameSeller = "Vendedor1",
                Cpf = "23214244124",
                Phone = "232321321312"
            };
        }
    }
}