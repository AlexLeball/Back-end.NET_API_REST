using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories.Interfaces;
using System.Collections.Generic;

namespace TestProject7
{
    [TestClass]
    public sealed class BidListControllerTests
    {
        private Mock<IBidListRepository> _mockRepo;
        private BidListController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IBidListRepository>();
            _controller = new BidListController(_mockRepo.Object);
        }

        private static BidList MakeBid(int id = 0, string account = "ACC", string bidType = "Type")
        {
            return new BidList
            {
                BidListId = id,
                Account = account,
                BidType = bidType,
                BidQuantity = 10,
                AskQuantity = 5,
                Bid = 1.23,
                Ask = 1.25,
                DealName = "Deal",
                DealType = "DealType",
                SourceListId = "SRC",
                Side = "Buy",
                Trader = "Trader"
            };
        }

        [TestMethod]
        public void CreateBid_ReturnsOk_WhenModelIsValid()
        {
            var bid = MakeBid();
            _mockRepo.Setup(r => r.Add(bid));

            var result = _controller.CreateBid(bid) as OkObjectResult;

            _mockRepo.Verify(r => r.Add(bid), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(bid, result.Value);
        }

        [TestMethod]
        public void ShowUpdateForm_ReturnsOk_WhenBidExists()
        {
            var bid = MakeBid(1);
            _mockRepo.Setup(r => r.GetById(1)).Returns(bid);

            var result = _controller.ShowUpdateForm(1) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(bid, result.Value);
        }

        [TestMethod]
        public void ShowUpdateForm_ReturnsNotFound_WhenBidDoesNotExist()
        {
            _mockRepo.Setup(r => r.GetById(1)).Returns((BidList)null);

            var result = _controller.ShowUpdateForm(1);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void UpdateBid_ReturnsOk_WhenUpdateSucceeds()
        {
            var bid = MakeBid(1, "NewAcc");
            _mockRepo.Setup(r => r.Update(1, bid)).Returns(true);
            _mockRepo.Setup(r => r.GetById(1)).Returns(bid);

            var result = _controller.UpdateBid(1, bid) as OkObjectResult;

            _mockRepo.Verify(r => r.Update(1, bid), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(bid, result.Value);
        }

        [TestMethod]
        public void UpdateBid_ReturnsNotFound_WhenUpdateFails()
        {
            var bid = MakeBid(999);
            _mockRepo.Setup(r => r.Update(999, bid)).Returns(false);

            var result = _controller.UpdateBid(999, bid);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteBid_ReturnsOk_WhenDeleteSucceeds()
        {
            _mockRepo.Setup(r => r.Delete(1)).Returns(true);

            var result = _controller.DeleteBid(1) as OkObjectResult;

            _mockRepo.Verify(r => r.Delete(1), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual("bid deleted", result.Value);
        }

        [TestMethod]
        public void DeleteBid_ReturnsNotFound_WhenDeleteFails()
        {
            _mockRepo.Setup(r => r.Delete(999)).Returns(false);

            var result = _controller.DeleteBid(999);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
