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
    public sealed class TradeControllerTests
    {
        private Mock<ITradeRepository> _mockRepo;
        private TradeController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<ITradeRepository>();
            _controller = new TradeController(_mockRepo.Object);
        }

        private static Trade MakeTrade(int id = 0, string account = "ACC", string trader = "Trader")
        {
            return new Trade
            {
                TradeId = id,
                Account = account,
                AccountType = "TypeA",
                BuyQuantity = 10,
                SellQuantity = 5,
                BuyPrice = 100,
                SellPrice = 105,
                Benchmark = "Bench",
                TradeSecurity = "Sec",
                TradeStatus = "Status",
                Trader = trader,
                Book = "Book1",
                TradeDate = System.DateTime.Now,
                CreationName = "Creator",
                CreationDate = System.DateTime.Now,
                RevisionDate = System.DateTime.Now,
                DealName = "Deal",
                DealType = "DealType",
                SourceListId = "SRC",
                Side = "Buy"
            };
        }

        [TestMethod]
        public void AddTrade_ReturnsOk_WhenModelIsValid()
        {
            var trade = MakeTrade();
            _mockRepo.Setup(r => r.Add(trade));

            var result = _controller.AddTrade(trade) as OkObjectResult;

            _mockRepo.Verify(r => r.Add(trade), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(trade, result.Value);
        }

        [TestMethod]
        public void FindTrade_ReturnsOk_WhenTradeExists()
        {
            var trade = MakeTrade(1);
            _mockRepo.Setup(r => r.GetById(1)).Returns(trade);

            var result = _controller.FindTrade(1) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(trade, result.Value);
        }

        [TestMethod]
        public void FindTrade_ReturnsNotFound_WhenTradeDoesNotExist()
        {
            _mockRepo.Setup(r => r.GetById(1)).Returns((Trade)null);

            var result = _controller.FindTrade(1);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void UpdateTrade_ReturnsOk_WhenUpdateSucceeds()
        {
            var trade = MakeTrade(1);
            _mockRepo.Setup(r => r.Update(1, trade)).Returns(true);
            _mockRepo.Setup(r => r.GetById(1)).Returns(trade);

            var result = _controller.UpdateTrade(1, trade) as OkObjectResult;

            _mockRepo.Verify(r => r.Update(1, trade), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(trade, result.Value);
        }

        [TestMethod]
        public void UpdateTrade_ReturnsNotFound_WhenUpdateFails()
        {
            var trade = MakeTrade(999);
            _mockRepo.Setup(r => r.Update(999, trade)).Returns(false);

            var result = _controller.UpdateTrade(999, trade);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteTrade_ReturnsOk_WhenDeleteSucceeds()
        {
            _mockRepo.Setup(r => r.Delete(1)).Returns(true);
            _mockRepo.Setup(r => r.GetAll()).Returns(new List<Trade>());

            var result = _controller.DeleteTrade(1) as OkObjectResult;

            _mockRepo.Verify(r => r.Delete(1), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(0, ((List<Trade>)result.Value).Count);
        }

        [TestMethod]
        public void DeleteTrade_ReturnsNotFound_WhenDeleteFails()
        {
            _mockRepo.Setup(r => r.Delete(999)).Returns(false);

            var result = _controller.DeleteTrade(999);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
