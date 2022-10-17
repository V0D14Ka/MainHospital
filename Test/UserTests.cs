using Domain.Models;
using System.Xml.Linq;

namespace Test
{
    public class UserTests
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public UserTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public void LoginIsEmptyOrNull_ShouldFail()
        {
            var res = _userService.GetUserByLogin(string.Empty);

            Assert.True(res.IsFailure); 
            Assert.Equal("Invalid login", res.Error);
        }

        [Fact]
        public void UserNotFound_ShouldFail()
        {
            _userRepositoryMock.Setup(repository => repository.GetUserByLogin(It.IsAny<string>()))
                .Returns(() => null);

            var res = _userService.GetUserByLogin("qwertyuiop");

            Assert.True(res.IsFailure);
            Assert.Equal("Unable to find user", res.Error); 
        }

        [Fact]
        public void AbstractRegister_ShouldFail()
        {
            User user = new User(1, "001", "Ivan", Role.Administrator, "Admin", "123");
            var res = _userService.Register(user);

            Assert.True(res.IsFailure); 
            Assert.Equal("Unable to create user", res.Error); 
        }
        [Fact]
        public void IsExistIsEmptyOrNull_ShouldFail()
        {
            var res = _userService.IsUserExists(string.Empty);

            Assert.True(res.IsFailure); 
            Assert.Equal("Invalid login", res.Error); 
        }
    }
}