using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using HoteliNovica.Controllers;
using HoteliNovica.Interfaces;
using HoteliNovica.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HoteliNovica.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetReturnsEmployeesWithSameId() //200(OK) i objekat
        {
            // Arange
            var mockRepository = new Mock<IHotelRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(new Hotel { Id = 1 });

            var controller = new HoteliController(mockRepository.Object);

            // Act  
            IHttpActionResult actionResult = controller.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<Hotel>;

            // Assert 
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Id);
        }

        [TestMethod]
        public void PutReturnsBadRequest() //400 BadRequest
        {
            // Arrange
            var mockRepository = new Mock<IHotelRepository>();
            var controller = new HoteliController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(6, new Hotel { Id = 9, Naziv = "Hotel 6" });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void GetReturnsMultipleObjects() //vraca kolekciju objekata
        {
            // Arrange
            List<Hotel> hotel = new List<Hotel>();
            hotel.Add(new Hotel { Id = 4, Naziv = "Novica Cankovic" });
            hotel.Add(new Hotel { Id = 5, Naziv = "Snezana Gardinovacki" });

            var mockRepository = new Mock<IHotelRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(hotel.AsEnumerable());
            var controller = new HoteliController(mockRepository.Object);

            // Act
            IEnumerable<Hotel> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(hotel.Count, result.ToList().Count);
            Assert.AreEqual(hotel.ElementAt(0), result.ElementAt(0));
            Assert.AreEqual(hotel.ElementAt(1), result.ElementAt(1));
        }

        [TestMethod]
        public void PostReturnsMultipleObjects() //vraca kolekciju objekata
        {
            // Arrange
            List<Hotel> employee = new List<Hotel>();
            employee.Add(new Hotel { Id = 4, Naziv = "Novica Cankovic" });
            employee.Add(new Hotel { Id = 5, Naziv = "Snezana Gardinovacki" });

            var mockRepository = new Mock<IHotelRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(employee.AsEnumerable());
            var controller = new HoteliController(mockRepository.Object);

            // Act
            IEnumerable<Hotel> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(employee.Count, result.ToList().Count);
            Assert.AreEqual(employee.ElementAt(0), result.ElementAt(0));
            Assert.AreEqual(employee.ElementAt(1), result.ElementAt(1));
        }




    }
}
