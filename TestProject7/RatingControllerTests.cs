using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace TestProject7
{
    [TestClass]
    public sealed class RatingControllerTests
    {
        private Mock<IRatingRepository> _mockRepo;
        private RatingController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IRatingRepository>();
            _controller = new RatingController(_mockRepo.Object);
        }

        private static Rating MakeRating(int id = 0)
        {
            return new Rating
            {
                Id = id,
                MoodysRating = "Aaa",
                SandPRating = "AAA",
                FitchRating = "AAA",
                OrderNumber = 1
            };
        }

        [TestMethod]
        public void Add_ReturnsOk_WhenModelIsValid()
        {
            var rating = MakeRating();
            _mockRepo.Setup(r => r.Add(rating));

            var result = _controller.Add(rating) as OkObjectResult;

            _mockRepo.Verify(r => r.Add(rating), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(rating, result.Value);
        }

        [TestMethod]
        public void GetById_ReturnsOk_WhenExists()
        {
            var rating = MakeRating(1);
            _mockRepo.Setup(r => r.GetById(1)).Returns(rating);

            var result = _controller.GetById(1) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(rating, result.Value);
        }

        [TestMethod]
        public void GetById_ReturnsNotFound_WhenNotExists()
        {
            _mockRepo.Setup(r => r.GetById(1)).Returns((Rating)null);

            var result = _controller.GetById(1);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void UpdateRating_ReturnsOk_WhenUpdateSucceeds()
        {
            var rating = MakeRating(1);
            _mockRepo.Setup(r => r.Update(1, rating)).Returns(true);
            _mockRepo.Setup(r => r.GetById(1)).Returns(rating);

            var result = _controller.UpdateRating(1, rating) as OkObjectResult;

            _mockRepo.Verify(r => r.Update(1, rating), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(rating, result.Value);
        }

        [TestMethod]
        public void UpdateRating_ReturnsNotFound_WhenUpdateFails()
        {
            var rating = MakeRating(999);
            _mockRepo.Setup(r => r.Update(999, rating)).Returns(false);

            var result = _controller.UpdateRating(999, rating);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteRating_ReturnsOk_WhenDeleteSucceeds()
        {
            _mockRepo.Setup(r => r.Delete(1)).Returns(true);
            _mockRepo.Setup(r => r.GetAll()).Returns(new List<Rating>());

            var result = _controller.DeleteRating(1) as OkObjectResult;

            _mockRepo.Verify(r => r.Delete(1), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(0, ((List<Rating>)result.Value).Count);
        }

        [TestMethod]
        public void DeleteRating_ReturnsNotFound_WhenDeleteFails()
        {
            _mockRepo.Setup(r => r.Delete(999)).Returns(false);

            var result = _controller.DeleteRating(999);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
