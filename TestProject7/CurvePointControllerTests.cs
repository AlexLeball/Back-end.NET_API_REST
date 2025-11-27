using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TestProject7
{
    [TestClass]
    public sealed class CurveControllerTests
    {
        private Mock<ICurvePointRepository> _mockRepo;
        private CurveController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<ICurvePointRepository>();
            _controller = new CurveController(_mockRepo.Object);
        }

        private static CurvePoint MakeCurvePoint(int id = 0, byte curveId = 1)
        {
            return new CurvePoint
            {
                Id = id,
                CurveId = curveId,
                AsOfDate = DateTime.Today,
                Term = 1.5,
                CurvePointValue = 2.5,
                CreationDate = DateTime.Now
            };
        }

        [TestMethod]
        public void GetAllCurvePoints_ReturnsOkWithData()
        {
            var data = new List<CurvePoint> { MakeCurvePoint(1), MakeCurvePoint(2) };
            _mockRepo.Setup(r => r.GetAll()).Returns(data);

            var result = _controller.GetAllCurvePoints() as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(data, result.Value);
        }

        [TestMethod]
        public void AddCurvePoint_ReturnsOk_WhenModelIsValid()
        {
            var curvePoint = MakeCurvePoint();
            _mockRepo.Setup(r => r.Add(curvePoint));
            _mockRepo.Setup(r => r.GetAll()).Returns(new List<CurvePoint> { curvePoint });

            var result = _controller.AddCurvePoint(curvePoint) as OkObjectResult;

            _mockRepo.Verify(r => r.Add(curvePoint), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(1, ((List<CurvePoint>)result.Value).Count);
        }

        [TestMethod]
        public void GetCurvePoint_ReturnsOk_WhenExists()
        {
            var curvePoint = MakeCurvePoint(1);
            _mockRepo.Setup(r => r.GetById(1)).Returns(curvePoint);

            var result = _controller.GetCurvePoint(1) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(curvePoint, result.Value);
        }

        [TestMethod]
        public void GetCurvePoint_ReturnsNotFound_WhenNotExists()
        {
            _mockRepo.Setup(r => r.GetById(1)).Returns((CurvePoint)null);

            var result = _controller.GetCurvePoint(1);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void UpdateCurvePoint_ReturnsOk_WhenUpdateSucceeds()
        {
            var curvePoint = MakeCurvePoint(1);
            _mockRepo.Setup(r => r.Update(1, curvePoint)).Returns(true);
            _mockRepo.Setup(r => r.GetAll()).Returns(new List<CurvePoint> { curvePoint });

            var result = _controller.UpdateCurvePoint(1, curvePoint) as OkObjectResult;

            _mockRepo.Verify(r => r.Update(1, curvePoint), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(1, ((List<CurvePoint>)result.Value).Count);
        }

        [TestMethod]
        public void UpdateCurvePoint_ReturnsNotFound_WhenUpdateFails()
        {
            var curvePoint = MakeCurvePoint(999);
            _mockRepo.Setup(r => r.Update(999, curvePoint)).Returns(false);

            var result = _controller.UpdateCurvePoint(999, curvePoint);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteCurvePoint_ReturnsOk_WhenDeleteSucceeds()
        {
            var curvePoint = MakeCurvePoint(1);
            _mockRepo.Setup(r => r.Delete(1)).Returns(true);
            _mockRepo.Setup(r => r.GetAll()).Returns(new List<CurvePoint>());

            var result = _controller.DeleteCurvePoint(1) as OkObjectResult;

            _mockRepo.Verify(r => r.Delete(1), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(0, ((List<CurvePoint>)result.Value).Count);
        }

        [TestMethod]
        public void DeleteCurvePoint_ReturnsNotFound_WhenDeleteFails()
        {
            _mockRepo.Setup(r => r.Delete(999)).Returns(false);

            var result = _controller.DeleteCurvePoint(999);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
