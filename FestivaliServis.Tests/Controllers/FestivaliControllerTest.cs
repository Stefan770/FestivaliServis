using FestivaliServis.Controllers;
using FestivaliServis.Models;
using FestivaliServis.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace FestivaliServis.Tests.Controllers
{
    [TestClass]
    public class FestivaliControllerTest
    {
        [TestMethod]
        public void GetReturnsOkAndObject()
        {
            // Arrange
            var mockRepository = new Mock<IFestivalRepo>();
            mockRepository.Setup(x => x.GetById(1)).Returns(new Festival { Id = 1, Naziv = "Exit" });

            var controller = new FestivaliController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<Festival>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        // --------------------------------------------------------------------------------------

        [TestMethod]
        public void GetReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IFestivalRepo>();
            var controller = new FestivaliController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        // --------------------------------------------------------------------------------------

        [TestMethod]
        public void DeleteReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IFestivalRepo>();
            mockRepository.Setup(x => x.GetById(1)).Returns(new Festival { Id = 1, Naziv = "Exit" });
            var controller = new FestivaliController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        // -------------------------------------------------------------------------------------

        [TestMethod]
        public void PostMethodReturnsOkAndObject()
        {
            // Arrange
            var mockRepository = new Mock<IFestivalRepo>();
            var controller = new FestivaliController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Post(new Festival { Id = 1, Naziv = "Exit" });
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Festival>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.IsNotNull(createdResult.Content);
        }

    }
}
