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

namespace ControllerCar.Tests
{
    [TestClass]
    public class ControllerCarTest
    {
        [Fact]
        public void IndexReturnsAViewResultWithAListOfCar()
        {

            var mock = new Mock<ICarRepository>();
            mock.Setup(repo => repo.GetAll()).Returns(GetTestCars());
            var controller = new CarController(mock.Object);


            var result = controller.Index();


            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            var model = Xunit.Assert.IsAssignableFrom<IEnumerable<Car>>(viewResult.Model);
            Xunit.Assert.Equal(GetTestCars().Count, model.Count());
        }
        private List<Car> GetTestCars()
        {
            var cars = new List<Car>
            {
                new Car { Type= "BMWw", Year = 2008},
                new Car { Type= "BMWww", Year = 2009},
                new Car { Type= "BMWwww", Year = 2010},
                new Car { Type= "BMWwwww", Year = 2011}
            };
            return cars;
        }
        [Fact]
        public void AddCarReturnsViewResultWithCarModel()
        {

            var mock = new Mock<ICarRepository>();
            var controller = new CarController(mock.Object);
            controller.ModelState.AddModelError("Name", "Required");
            Car newCar = new Car();


            var result = controller.AddCar(newCar);

 
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            Xunit.Assert.Equal(newCar, viewResult?.Model);
        }

        [Fact]
        public void AddCarReturnsARedirectAndAddsCar()
        {

            var mock = new Mock<ICarRepository>();
            var controller = new CarController(mock.Object);
            var newUser = new Car()
            {
                Type = "Jaguar",
                Year = 2019
            };

            var result = controller.AddCar(newUser);

            var redirectToActionResult = Xunit.Assert.IsType<RedirectToActionResult>(result);
            Xunit.Assert.Null(redirectToActionResult.ControllerName);
            Xunit.Assert.Equal("Index", redirectToActionResult.ActionName);
            mock.Verify(r => r.Create(newUser));
        }
        [Fact]
        public void GetCarReturnsBadRequestResultWhenIdIsNull()
        {

            var mock = new Mock<ICarRepository>();
            var controller = new CarController(mock.Object);

 
            var result = controller.GetCar(null);

            Xunit.Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public void GetCarReturnsNotFoundResultWhenCarNotFound()
        {
            int testCarId = 1;
            var mock = new Mock<ICarRepository>();
            mock.Setup(repo => repo.Get(testCarId))
                .Returns(null as Car);
            var controller = new CarController(mock.Object);

            var result = controller.GetCar(testCarId);

            Xunit.Assert.IsType<NotFoundResult>(result);
        }

       
    }
}