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

namespace ControllerDiscount.Tests
{
    [TestClass]
    public class ControllerDiscountTest
    {
        [Fact]
        public void IndexReturnsAViewResultWithAListOfDiscount()
        {

            var mock = new Mock<IDiscountRepository>();
            mock.Setup(repo => repo.GetAll()).Returns(GetTestDiscounts());
            var controller = new DiscountController(mock.Object);


            var result = controller.Index();


            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            var model = Xunit.Assert.IsAssignableFrom<IEnumerable<Discount>>(viewResult.Model);
            Xunit.Assert.Equal(GetTestDiscounts().Count, model.Count());
        }
        private List<Discount> GetTestDiscounts()
        {
            var discounts = new List<Discount>
            {
                new Discount {  Id=1, SizeOfDiscount = "20", UserId = "1"},
                new Discount {  Id=2,   SizeOfDiscount = "30", UserId ="1"},
                new Discount {  Id=3,   SizeOfDiscount = "45", UserId = "1"},
                new Discount {  Id=4,  SizeOfDiscount = "10", UserId = "1"}
            };
            return discounts;
        }
        [Fact]
        public void AddDiscountReturnsViewResultWithDiscountModel()
        {

            var mock = new Mock<IDiscountRepository>();
            var controller = new DiscountController(mock.Object);
            controller.ModelState.AddModelError("Name", "Required");
            Discount newDiscount = new Discount();


            var result = controller.AddDiscount(newDiscount);


            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            Xunit.Assert.Equal(newDiscount, viewResult?.Model);
        }

        [Fact]
        public void AddDiscountReturnsARedirectAndAddsDiscount()
        {

            var mock = new Mock<IDiscountRepository>();
            var controller = new DiscountController(mock.Object);
            var newDiscount = new Discount()
            {
                Id=5,
                SizeOfDiscount = "1",
                UserId = "1"
            };

            var result = controller.AddDiscount(newDiscount);

            var redirectToActionResult = Xunit.Assert.IsType<RedirectToActionResult>(result);
            Xunit.Assert.Null(redirectToActionResult.ControllerName);
            Xunit.Assert.Equal("Index", redirectToActionResult.ActionName);
            mock.Verify(r => r.Create(newDiscount));
        }
        [Fact]
        public void GetDiscountReturnsBadRequestResultWhenIdIsNull()
        {
          
            var mock = new Mock<IDiscountRepository>();
            var controller = new DiscountController(mock.Object);

     
            var result = controller.GetDiscount(null);

    
            Xunit.Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public void GetDiscountReturnsNotFoundResultWhenDiscountNotFound()
        {
            int testDiscountId = 1;
            var mock = new Mock<IDiscountRepository>();
            mock.Setup(repo => repo.Get(testDiscountId))
                .Returns(null as Discount);
            var controller = new DiscountController(mock.Object);

            var result = controller.GetDiscount(testDiscountId);

            Xunit.Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void GetDiscountReturnsViewResultWithDiscount()
        {
            
            int testDiscountId = 1;
            var mock = new Mock<IDiscountRepository>();
            mock.Setup(repo => repo.Get(testDiscountId))
                .Returns(GetTestDiscounts().FirstOrDefault(p => p.Id == testDiscountId));
            var controller = new DiscountController(mock.Object);

         
            var result = controller.GetDiscount(testDiscountId);

           
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            var model = Xunit.Assert.IsType<Discount>(viewResult.ViewData.Model);
            Xunit.Assert.Equal("1", model.UserId);
            Xunit.Assert.Equal("20", model.SizeOfDiscount);
            Xunit.Assert.Equal(testDiscountId, model.Id);
        }

    }
}