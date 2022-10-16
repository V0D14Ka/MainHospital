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
            // Используем библиотеку Moq, чтобы подготавливать тестовые данные
            // Мы отдаем реализацию интерфейса, но сервису без разницы, что там, ему важно, что удовлетворяется интерфейсу
            // Таким образом мы подкидываем нужные данные для тестовых сценариев, другими словами, "мокаем" (mock) репозиторий 
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public void LoginIsEmptyOrNull_ShouldFail()
        {
            var res = _userService.GetUserByLogin(string.Empty);

            Assert.True(res.IsFailure); // Ошибка пустой строки
            Assert.Equal("Invalid login", res.Error);
        }

        [Fact]
        public void UserNotFound_ShouldFail()
        {
            // It.IsAny означает, что мы подготавливаем то, что должен вернуть метод вне зависимости от того, какой логин мы указали
            // То есть в данном случае, можно скормить любой string, так как мы тестируем сценарий ненахода, нам в любом случае надо вернуть нуль
            _userRepositoryMock.Setup(repository => repository.GetUserByLogin(It.IsAny<string>()))
                .Returns(() => null);

            var res = _userService.GetUserByLogin("qwertyuiop");

            Assert.True(res.IsFailure);
            Assert.Equal("Unable to find user", res.Error); 
        }

        [Fact]
        public void AbstractRegiter_ShouldFail()
        {
            User user = new User(1, "001", "Ivan", Role.Administrator, "Admin", "123");
            var res = _userService.Register(user);

            Assert.True(res.IsFailure); // Вернет ошибку т.к интерфейс не реализован
            Assert.Equal("Unable to create user", res.Error); 
        }
        [Fact]
        public void IsExistIsEmptyOrNull_ShouldFail()
        {
            var res = _userService.IsUserExists(string.Empty);

            Assert.True(res.IsFailure); // Вернет ошибку пустой строки
            Assert.Equal("Invalid login", res.Error); 
        }
    }
}