using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using Bono.Orders.Application.AutoMapper;
using Bono.Orders.Application.Services;
using Bono.Orders.Application.ViewModels;
using Bono.Orders.Domain.Entities;
using Xunit;
using Bono.Orders.Domain.Interfaces.Repository;
using ValidationResult = Bono.Orders.Domain.Validations.ValidationResult;
using System.Linq;

namespace Bono.Orders.Application.Tests.Unit.Services
{
    public class OrderServiceTests
    {
        private readonly OrderService OrderService;

        public OrderServiceTests()
        {
            OrderService = new OrderService(new Mock<IOrderRepository>().Object, new Mock<IUserRepository>().Object, new Mock<IOrderTypeRepository>().Object, new Mock<IMapper>().Object, new Mock<ValidationResult>().Object);
        }

        #region ValidatingSendingID

        [Fact]
        public void PostSendingValidId()
        {            
            var result = OrderService.Post(new OrderViewModel { Id = Guid.NewGuid() });
            Assert.Contains("OrderID must be empty", result.Errors.Select(x => x.Message).ToList());
        }

        [Fact]
        public void GetByIdSendingEmptyGuid()
        {
            var exception = Assert.Throws<Exception>(() => OrderService.GetById(""));
            Assert.Equal("OrderID is not valid", exception.Message);
        }

        [Fact]
        public void PutSendingEmptyGuid()
        {
            var result = OrderService.Put(new OrderViewModel());
            Assert.Contains("OrderID is not valid", result.Errors.Select(x => x.Message).ToList());
        }

        [Fact]
        public void DeleteSendingEmptyGuid()
        {            
            var result = OrderService.Delete("");
            Assert.Contains("OrderID is not valid", result.Errors.Select(x => x.Message).ToList());            
        }

        #endregion

        #region ValidatingCorrectObject

        [Fact]
        public void PostSendingValidObject()
        {
            var result = OrderService.Post(new OrderViewModel { CustomerName = "Richie Bono", UserId = Guid.NewGuid().ToString(), OrderTypeId = Guid.NewGuid().ToString() });
            Assert.True(result.IsValid);
        }

        [Fact]
        public void GetValidatingObject()
        {
            var user = new User("Richard Bono", "admin@123", "Richard Bono", "Oliveira", "123.456.456-56", "richiebono@gmail.com", "+55 11-98547-3851");

            List<Order> Orders = new()
            {
                new Order(new OrderType("Standard"), "Client 1", user)
            };
            
            var OrderRepository = new Mock<IOrderRepository>();
            OrderRepository.Setup(x => x.GetAll()).Returns(Orders);
            var autoMapperProfile = new AutoMapperSetup();
            var configuration = new MapperConfiguration(x => x.AddProfile(autoMapperProfile));
            IMapper mapper = new Mapper(configuration);
            var result = OrderRepository.Object.GetAll();
            Assert.True(result.Any());
        }

        #endregion

        #region ValidatingRequiredFields

        [Fact]
        public void PostSendingInvalidObject()
        {
            OrderViewModel Order = new();

            var result = OrderService.Post(Order);
            Assert.Contains("The sent object was empty.", result.Errors.Select(x => x.Message).ToList());            
        }

        #endregion
    }
}
