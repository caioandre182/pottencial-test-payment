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
using FluentAssertions;
using Tech.Test.Payment.Tests.Fixtures;

namespace Tech.Test.Payment.Tests.Projects.ApplicationCreate
{
    public class CreateSaleServiceTests
    {
        //Subject Under Test
        private readonly ISaleService _sut;

        //Mocks
        private readonly IMapper _mapper;
        private readonly Mock<ISaleRepository> _saleRepositoryMock;

        public CreateSaleServiceTests()
        {
            _mapper = AutoMapperConfiguration.GetConfiguration();
            _saleRepositoryMock = new Mock<ISaleRepository>();

            _sut = new SaleService(
                mapper: _mapper,
                saleRepository: _saleRepositoryMock.Object
            );
        }

        [Fact(DisplayName = "Create Valid Sale")]
        [Trait("Category","Services")]
        public async Task Create_WhenSaleIsValid_True()
        {
            //Arrange
            var saleToCreate = SaleFixture.CreateValidSaleDTO();

            saleToCreate.Date = DateTime.Now;
            saleToCreate.Status = 0;

            var saleCreated = _mapper.Map<Sale>(saleToCreate);

            //Act
            var result = await _sut.CreateAsync(saleToCreate);

            //Assert
            Assert.True(result.IsSucess);
        }

        [Fact(DisplayName = "Create InValid Sale")]
        [Trait("Category","Services")]
        public async Task Create_WhenSaleIsNotValid_ReturnsFalse()
        {
            //Arrange
            var saleToCreate = SaleFixture.CreateValidSaleDTO();

            //SEM DATA

            saleToCreate.Status = StatusEnum.AWAITING_PAYMENT;

            var saleCreated = _mapper.Map<Sale>(saleToCreate);

            //Act
            var result = await _sut.CreateAsync(saleToCreate);

            //Assert
            Assert.False(result.IsSucess);
        }

        [Theory(DisplayName = "Create Sale Whit Status Diffente of AWAITING_PAYMENT")]
        [InlineData(StatusEnum.PAYMENT_ACCEPTED)]
        [InlineData(StatusEnum.SENT_FOR_TRANSPORT)]
        [InlineData(StatusEnum.CANCELED)]
        [InlineData(StatusEnum.DELIVERED)]
        [Trait("Category","Services")]
        public async Task Create_WhenStatusDifferenteAwaitingPayment_ReturnsFalse(StatusEnum status)
        {
            //Arrange
            var saleToCreate = SaleFixture.CreateValidSaleDTO();

            saleToCreate.Date = DateTime.Now;
            saleToCreate.Status = status;

            var saleCreated = _mapper.Map<Sale>(saleToCreate);

            //Act
            var result = await _sut.CreateAsync(saleToCreate);

            //Assert
            Assert.False(result.IsSucess);
        }

        [Fact(DisplayName = "Create Sale With Item Quantity Less Than 1")]
        [Trait("Category","Services")]
        public async Task Create_ItemQuantityLessThanOne_ReturnsFalse()
        {
            //Arrange
            var saleToCreate = SaleFixture.CreateValidSaleDTO();

            saleToCreate.QuantityItems = 0; //!!!!!!
            saleToCreate.Date = DateTime.Now;
            saleToCreate.Status = StatusEnum.AWAITING_PAYMENT;

            var saleCreated = _mapper.Map<Sale>(saleToCreate);

            //Act
            var result = await _sut.CreateAsync(saleToCreate);

            //Assert
            Assert.False(result.IsSucess);
        }
        
    }
}