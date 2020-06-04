using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using OnionApp.Domain.Core;
using OnionApp.Domain.Interfaces;
using OnionApp.Infrastructure.Data;
using ProjectTaxi.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace OrdersController.Tests
{
    [TestClass]
    public class ControllerOrderTest
    {
        [Fact]
        public void IndexReturnsAViewResultWithAListOfOrder()
        {

            var mock = new Mock<IOrderRepository>();
            mock.Setup(repo => repo.GetAll()).Returns(GetTestOrders());
            var controller = new OrderController(mock.Object);


            var result = controller.Index();


            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            var model = Xunit.Assert.IsAssignableFrom<IEnumerable<Order>>(viewResult.Model);
            Xunit.Assert.Equal(GetTestOrders().Count, model.Count());
        }
        private List<Order> GetTestOrders()
        {
            var orders = new List<Order>
            {
                new Order {  From="Chernivtsi", To = "Lviv", Price = 280, IdUser=1, IdTaxist=2},
                new Order {  From="Chernivtsi2", To = "Lviv2", Price = 270, IdUser=2, IdTaxist=3},
                new Order {  From="Chernivtsi3", To = "Lviv3", Price = 260, IdUser=3, IdTaxist=4},
                new Order {  From="Chernivtsi4", To = "Lviv4", Price = 250, IdUser=4, IdTaxist=5}
            };
            return orders;
        }
        [Fact]
        public void AddOrderReturnsViewResultWithOrderModel()
        {

            var mock = new Mock<IOrderRepository>();
            var controller = new OrderController(mock.Object);
            controller.ModelState.AddModelError("Name", "Required");
            Order newOrder = new Order();


            var result = controller.AddOrder(newOrder);


            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            Xunit.Assert.Equal(newOrder, viewResult?.Model);
        }

        [Fact]
        public void AddOrderReturnsARedirectAndAddsOrder()
        {

            var mock = new Mock<IOrderRepository>();
            var controller = new OrderController(mock.Object);
            var newOrder = new Order()
            {
                From = "Chernivtsi",
                To = "Lviv",
                Price = 280,
                IdUser = 1,
                IdTaxist = 2
            };

            var result = controller.AddOrder(newOrder);

            var redirectToActionResult = Xunit.Assert.IsType<RedirectToActionResult>(result);
            Xunit.Assert.Null(redirectToActionResult.ControllerName);
            Xunit.Assert.Equal("Index", redirectToActionResult.ActionName);
            mock.Verify(r => r.Create(newOrder));
        }
        [Fact]
        public void GetOrderReturnsBadRequestResultWhenIdIsNull()
        {

            var mock = new Mock<IOrderRepository>();
            var controller = new OrderController(mock.Object);


            var result = controller.GetOrder(null);


            Xunit.Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public void GetOrderReturnsNotFoundResultWhenOrderNotFound()
        {
            int testOrderId = 1;
            var mock = new Mock<IOrderRepository>();
            mock.Setup(repo => repo.Get(testOrderId))
                .Returns(null as Order);
            var controller = new OrderController(mock.Object);

            var result = controller.GetOrder(testOrderId);

            Xunit.Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void GetOrderReturnsViewResultWithOrder()
        {

            int testOrderId = 1;
            var mock = new Mock<IOrderRepository>();
            mock.Setup(repo => repo.Get(testOrderId))
                .Returns(GetTestOrders().FirstOrDefault(p => p.IdUser == testOrderId));
            var controller = new OrderController(mock.Object);


            var result = controller.GetOrder(testOrderId);


            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            var model = Xunit.Assert.IsType<Order>(viewResult.ViewData.Model);
            Xunit.Assert.Equal("Chernivtsi", model.From);
            Xunit.Assert.Equal("Lviv", model.To);
            Xunit.Assert.Equal(280, model.Price);
            Xunit.Assert.Equal(2, model.IdTaxist);
            Xunit.Assert.Equal(testOrderId, model.IdUser);
        }
    }
}