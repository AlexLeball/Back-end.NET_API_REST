using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Identity;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject7
{
    [TestClass]
    public class UserRepositoryTests
    {
        private Mock<UserManager<User>> _mockUserManager;
        private Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private UserRepository _userRepository;

        [TestInitialize]
        public void Setup()
        {
            var userStore = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(
                userStore.Object, null, null, null, null, null, null, null, null);

            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(
                roleStore.Object, null, null, null, null);

            _userRepository = new UserRepository(_mockUserManager.Object, _mockRoleManager.Object);
        }

        [TestMethod]
        public void FindAll_ReturnsAllUsers()
        {
            var users = new List<User>
            {
                new User { UserName = "User1" },
                new User { UserName = "User2" }
            }.AsQueryable();

            _mockUserManager.Setup(u => u.Users).Returns(users);

            var result = _userRepository.FindAll().ToList();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("User1", result[0].UserName);
        }

        [TestMethod]
        public async Task AddAsync_CreatesUserSuccessfully()
        {
            var user = new User { UserName = "NewUser" };
            var password = "Password123!";

            _mockUserManager.Setup(u => u.CreateAsync(user, password))
                            .ReturnsAsync(IdentityResult.Success);

            _mockRoleManager.Setup(r => r.RoleExistsAsync("User")).ReturnsAsync(true);
            _mockUserManager.Setup(u => u.AddToRoleAsync(user, "User"))
                            .ReturnsAsync(IdentityResult.Success);

            var result = await _userRepository.AddAsync(user, password);

            Assert.IsTrue(result.Succeeded);
            _mockUserManager.Verify(u => u.CreateAsync(user, password), Times.Once);
            _mockUserManager.Verify(u => u.AddToRoleAsync(user, "User"), Times.Once);
        }

        [TestMethod]
        public async Task AddAsync_Fails_WhenRoleAssignmentFails()
        {
            var user = new User { UserName = "FailUser" };
            var password = "Password123!";

            _mockUserManager.Setup(u => u.CreateAsync(user, password))
                            .ReturnsAsync(IdentityResult.Success);

            _mockRoleManager.Setup(r => r.RoleExistsAsync("User")).ReturnsAsync(true);
            _mockUserManager.Setup(u => u.AddToRoleAsync(user, "User"))
                            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Role failed" }));

            _mockUserManager.Setup(u => u.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

            var result = await _userRepository.AddAsync(user, password);

            Assert.IsFalse(result.Succeeded);
            Assert.AreEqual("Role failed", result.Errors.First().Description);
            _mockUserManager.Verify(u => u.DeleteAsync(user), Times.Once);
        }

        [TestMethod]
        public async Task UpdateAsync_ReturnsSuccess()
        {
            var user = new User { UserName = "User1" };
            _mockUserManager.Setup(u => u.UpdateAsync(user)).ReturnsAsync(IdentityResult.Success);

            var result = await _userRepository.UpdateAsync(user);

            Assert.IsTrue(result.Succeeded);
            _mockUserManager.Verify(u => u.UpdateAsync(user), Times.Once);
        }
    }
}
