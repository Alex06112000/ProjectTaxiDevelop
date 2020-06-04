using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using OnionApp.Domain.Core;
using OnionApp.Domain.Interfaces;
using OnionApp.Infrastructure.Data;
using ProjectTaxi.Controllers;
using ProjectTaxi.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit.Sdk;

namespace CarControllerTests
{   [TestClass]
    public class CarControllerTests
    {

        [TestMethod]
        public async Task GetProductAsync_ShouldReturnCorrectProduct()
        {
            var testProducts = GetTestProducts();
            var controller = new CarController(testProducts);

            var result = await controller.Get(4);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(result);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(testProducts[3].Type, result.Value.Type);
        }

        private List<Car> GetTestProducts()
        {
            var testCars = new List<Car>();
            testCars.Add(new Car { IdCar = 4, Type = "Demo1"});
            return testCars;
        }
    }
}