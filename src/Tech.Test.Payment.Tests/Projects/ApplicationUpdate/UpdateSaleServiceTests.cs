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

namespace Tech.Test.Payment.Tests.Projects.ApplicationUpdate
{
    public class UpdateSaleServiceTests
    {
        //Subject Under Test
        private readonly ISaleService _sut;

        //Mocks
        private readonly IMapper _mapper;
        private readonly Mock<ISaleRepository> _saleRepositoryMock;

        public UpdateSaleServiceTests()
        {
            _mapper = AutoMapperConfiguration.GetConfiguration();
            _saleRepositoryMock = new Mock<ISaleRepository>();

            _sut = new SaleService(
                mapper: _mapper,
                saleRepository: _saleRepositoryMock.Object
            );
        }

        [Fact(DisplayName = "Update Valid User")]
        [Trait("Category", "Services")]
        public async Task Update_WhenSaleIsValid_ReturnsTrue()
        {
            //Arrange
            int saleId = new Random().Next();
            var oldSale = SaleFixture.CreateValidSale();

            _saleRepositoryMock.Setup(x => x.GetStatus(saleId))
                 .ReturnsAsync(() => oldSale);

            _saleRepositoryMock.Setup(x => x.GetByIdAsync(saleId))
                 .ReturnsAsync(() => oldSale);

            var saleToUpdate = SaleFixture.CreateValidSaleDTO();
            saleToUpdate.Status = StatusEnum.PAYMENT_ACCEPTED;
            saleToUpdate.Id = saleId;
            var saleUpdated = _mapper.Map<Sale>(saleToUpdate);

            //Act
            var result = await _sut.UpdateAsync(saleToUpdate);

            //Assert
            Assert.True(result.IsSucess);

        }

        [Theory(DisplayName = "Update When Status AWAINTG_PAYMENT For PAYMENT_ACCEPTED Or CANCELED ")]
        [InlineData(StatusEnum.PAYMENT_ACCEPTED)]
        [InlineData(StatusEnum.CANCELED)]
        [Trait("Category", "Services")]
        public async Task Update_WhenStatusAwaintgPaymentForPaymentAcceptedOrCanceled_ReturnsTrue(StatusEnum status)
        {
            //Arrange
            int saleId = new Random().Next();
            var oldSale = SaleFixture.CreateValidSale();

            _saleRepositoryMock.Setup(x => x.GetStatus(saleId))
                 .ReturnsAsync(() => oldSale);

            _saleRepositoryMock.Setup(x => x.GetByIdAsync(saleId))
                 .ReturnsAsync(() => oldSale);

            var saleToUpdate = SaleFixture.CreateValidSaleDTO();
            saleToUpdate.Status = status;
            saleToUpdate.Id = saleId;
            var saleUpdated = _mapper.Map<Sale>(saleToUpdate);

            //Act
            var result = await _sut.UpdateAsync(saleToUpdate);

            //Assert
            Assert.True(result.IsSucess);
        }

        [Theory(DisplayName = "Update When Status PAYMENT_ACCEPTED For SENT_FOR_TRANSPORT  Or CANCELED ")]
        [InlineData(StatusEnum.SENT_FOR_TRANSPORT)]
        [InlineData(StatusEnum.CANCELED)]
        [Trait("Category", "Services")]
        public async Task Update_WhenStatusPaymentAcceptedForCanceledOrSentForTransport_ReturnsTrue(StatusEnum status)
        {
            //Arrange
            int saleId = new Random().Next();
            var oldSale = SaleFixture.CreateValidSale();
            oldSale.Status = StatusEnum.PAYMENT_ACCEPTED;

            _saleRepositoryMock.Setup(x => x.GetStatus(saleId))
                 .ReturnsAsync(() => oldSale);

            _saleRepositoryMock.Setup(x => x.GetByIdAsync(saleId))
                 .ReturnsAsync(() => oldSale);

            var saleToUpdate = SaleFixture.CreateValidSaleDTO();
            saleToUpdate.Status = status;
            saleToUpdate.Id = saleId;
            var saleUpdated = _mapper.Map<Sale>(saleToUpdate);

            //Act
            var result = await _sut.UpdateAsync(saleToUpdate);

            //Assert
            Assert.True(result.IsSucess);
        }

        [Fact(DisplayName = "Update When Status SENT_FOR_TRANSPORT For DELIVERED ")]
        [Trait("Category", "Services")]
        public async Task Update_WhenStatusSentForTransportForDelivered_ReturnsTrue()
        {
            //Arrange
            int saleId = new Random().Next();
            var oldSale = SaleFixture.CreateValidSale();

            oldSale.Status = StatusEnum.SENT_FOR_TRANSPORT;

            _saleRepositoryMock.Setup(x => x.GetStatus(saleId))
                 .ReturnsAsync(() => oldSale);

            _saleRepositoryMock.Setup(x => x.GetByIdAsync(saleId))
                 .ReturnsAsync(() => oldSale);

            var saleToUpdate = SaleFixture.CreateValidSaleDTO();
            saleToUpdate.Status = StatusEnum.DELIVERED;
            saleToUpdate.Id = saleId;
            var saleUpdated = _mapper.Map<Sale>(saleToUpdate);

            //Act
            var result = await _sut.UpdateAsync(saleToUpdate);

            //Assert
            Assert.True(result.IsSucess);
        }

                [Fact(DisplayName = "Update Status When Canceled ")]
        [Trait("Category", "Services")]
        public async Task Update_WhenStatusCanceled_ReturnsFalse()
        {
            //Arrange
            int saleId = new Random().Next();
            var oldSale = SaleFixture.CreateValidSale();

            oldSale.Status = StatusEnum.SENT_FOR_TRANSPORT;

            _saleRepositoryMock.Setup(x => x.GetStatus(saleId))
                 .ReturnsAsync(() => oldSale);

            _saleRepositoryMock.Setup(x => x.GetByIdAsync(saleId))
                 .ReturnsAsync(() => oldSale);

            var saleToUpdate = SaleFixture.CreateValidSaleDTO();
            saleToUpdate.Status = StatusEnum.CANCELED;
            saleToUpdate.Id = saleId;
            var saleUpdated = _mapper.Map<Sale>(saleToUpdate);

            //Act
            var result = await _sut.UpdateAsync(saleToUpdate);

            //Assert
            Assert.False(result.IsSucess);
        }

    }
}