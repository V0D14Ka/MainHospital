using Microsoft.EntityFrameworkCore;
using DB;
using DB.Models;
using DB.Repositories;

namespace UnitTests.DatabaseTests
{
    public class EfPlayground
    {
        private readonly DbContextOptionsBuilder<ApplicationContext> _optionsBuilder;

        public EfPlayground()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseNpgsql(
                $"Host=localhost;Port=5432;Database=backend_db;Username=backend_user;Password=backend_pass");
            _optionsBuilder = optionsBuilder;
        }

        //[Fact]
        public void PlaygroundMethod4()
        {
            var context = new ApplicationContext(_optionsBuilder.Options);

            var userRep = new UserRepository(context);

            userRep.Create(new User(0, "123123", "fiofio", Role.Patient, "Name", "Pass"));

            context.SaveChanges();

            Assert.True(context.Users.Any(u => u.UserName == "Name"));
        }

        // [Fact]
        public void PlaygroundMethod1()
        {
            using var context = new ApplicationContext(_optionsBuilder.Options);
            context.Users.Add(new UserModel
            {
                Id = 123,
                UserName = "TEST"
            });
            context.SaveChanges();

            Assert.True(context.Users.Any(u => u.UserName == "TEST"));
        }

        //[Fact]
        public void PlaygroundMethod2()
        {
            using var context = new ApplicationContext(_optionsBuilder.Options);
            var u = context.Users.FirstOrDefault(u => u.UserName == "Name");
            context.Users.Remove(u);
            context.SaveChanges();

            Assert.True(!context.Users.Any(u => u.UserName == "Name"));
        }

        // [Fact]
        public void PlaygroundMethod3()
        {
            #region подготовили сервис

            using var context = new ApplicationContext(_optionsBuilder.Options);
            var userRepository = new UserRepository(context);
            var userService = new UserService(userRepository);

            #endregion

            var res = userService.GetUserByLogin("TEST");

            Assert.NotNull(res.Value);
        }
    }
}