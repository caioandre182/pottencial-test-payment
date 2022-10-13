using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Tech.Test.Payment.Application.DTO;
using Tech.Test.Payment.Application.Services;
using Tech.Test.Payment.Application.Services.Interfaces;
using Tech.Test.Payment.Domain.Entities;
using Tech.Test.Payment.Domain.Repositories;
using Tech.Test.Payment.Tests.Configuration;
using Tech.Test.Payment.Tests.Fixtures;

namespace Tech.Test.Payment.Tests.Projects.ApplicationGet
{
    public class GetSaleServiceTests
    {
        //Subject Under Test
        private readonly ISaleService _sut;

        //Mocks
        private readonly IMapper _mapper;
        private readonly Mock<ISaleRepository> _saleRepositoryMock;

        public GetSaleServiceTests()
        {
            _mapper = AutoMapperConfiguration.GetConfiguration();
            _saleRepositoryMock = new Mock<ISaleRepository>();

            _sut = new SaleService(
                mapper: _mapper,
                saleRepository: _saleRepositoryMock.Object
            );
        }

        [Fact(DisplayName = "Get By Id")]
        [Trait("Category", "Services")]
        public async Task Get_WhenIdIsValid_ReturnsTrue()
        {
            //Arrange
            int saleId = new Random().Next();
            var saleFound = SaleFixture.CreateValidSale();
            _saleRepositoryMock.Setup(x => x.GetByIdAsync(saleId))
                .ReturnsAsync(() => saleFound);

            //Act
            var result = await _sut.GetByIdAsync(saleId);

            //Arrange
            Assert.True(result.IsSucess);
        }

        [Fact(DisplayName = "Get By Id When Sale Not Exists")]
        [Trait("Category", "Services")]
        public async Task Get_WhenSaleNotExists_ReturnFalse()
        {
            //Arrange
            int saleId = new Random().Next();
            var saleFound = SaleFixture.CreateValidSale();
            _saleRepositoryMock.Setup(x => x.GetByIdAsync(saleId))
                .ReturnsAsync(() => null);

            //Act
            var result = await _sut.GetByIdAsync(saleId);

            //Arrange
            Assert.False(result.IsSucess);
        }
    }
}