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
    public sealed class RuleNameControllerTests
    {
        private Mock<IRuleNameRepository> _mockRepo = null!;
        private RuleNameController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            _mockRepo = new Mock<IRuleNameRepository>();
            _controller = new RuleNameController(_mockRepo.Object);
        }

        private static RuleName MakeRuleName(int id = 0, string name = "Rule")
        {
            return new RuleName
            {
                Id = id,
                Name = name,
                Description = "Desc",
                Json = "{}",
                Template = "Template",
                SqlStr = "SELECT 1",
                SqlPart = "WHERE 1=1"
            };
        }

        [TestMethod]
        public void AddRuleName_ReturnsOk_WhenModelIsValid()
        {
            var rule = MakeRuleName();
            _mockRepo.Setup(r => r.Add(rule));

            var result = _controller.AddRuleName(rule) as OkObjectResult;

            _mockRepo.Verify(r => r.Add(rule), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(rule, result.Value);
        }

        [TestMethod]
        public void GetRuleName_ReturnsOk_WhenExists()
        {
            var rule = MakeRuleName(1);
            _mockRepo.Setup(r => r.GetById(1)).Returns(rule);

            var result = _controller.GetRuleName(1) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(rule, result.Value);
        }

        [TestMethod]
        public void GetRuleName_ReturnsNotFound_WhenNotExists()
        {
            _mockRepo.Setup(r => r.GetById(1)).Returns((RuleName?)null);

            var result = _controller.GetRuleName(1);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void UpdateRuleName_ReturnsOk_WhenUpdateSucceeds()
        {
            var rule = MakeRuleName(1);
            _mockRepo.Setup(r => r.Update(1, rule)).Returns(true);
            _mockRepo.Setup(r => r.GetAll()).Returns(new List<RuleName> { rule });

            var result = _controller.UpdateRuleName(1, rule) as OkObjectResult;

            _mockRepo.Verify(r => r.Update(1, rule), Times.Once);
            Assert.IsNotNull(result);

            var list = result?.Value as List<RuleName>;
            Assert.IsNotNull(list);
            Assert.AreEqual(1, list!.Count);
        }

        [TestMethod]
        public void UpdateRuleName_ReturnsNotFound_WhenUpdateFails()
        {
            var rule = MakeRuleName(999);
            _mockRepo.Setup(r => r.Update(999, rule)).Returns(false);

            var result = _controller.UpdateRuleName(999, rule);

            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public void DeleteRuleName_ReturnsOk_WhenDeleteSucceeds()
        {
            _mockRepo.Setup(r => r.Delete(1)).Returns(true);
            _mockRepo.Setup(r => r.GetAll()).Returns(new List<RuleName>());

            var result = _controller.DeleteRuleName(1) as OkObjectResult;

            _mockRepo.Verify(r => r.Delete(1), Times.Once);
            Assert.IsNotNull(result);

            var list = result?.Value as List<RuleName>;
            Assert.IsNotNull(list);
            Assert.AreEqual(0, list!.Count);
        }

        [TestMethod]
        public void DeleteRuleName_ReturnsNotFound_WhenDeleteFails()
        {
            _mockRepo.Setup(r => r.Delete(999)).Returns(false);

            var result = _controller.DeleteRuleName(999);

            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        }
    }
}
