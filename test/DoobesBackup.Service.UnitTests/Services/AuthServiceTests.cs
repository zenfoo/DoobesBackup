namespace DoobesBackup.Domain.UnitTests.Services
{
    using DoobesBackup.Infrastructure.Repositories;
    using DoobesBackup.Service.Services;
    using Infrastructure;
    using Moq;
    using Nancy;
    using Nancy.Testing;
    using Service.Modules;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    /// <summary>
    /// Tests for the BackupSource class
    /// </summary>
    public class AuthServiceTests
    {
        /// <summary>
        /// Test the name property is assigned correctly
        /// </summary>
        [Fact]
        public async void Login_UserDoesntExist_ReturnsNull()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(mr => mr.GetByUserName(It.IsAny<string>())).Returns(Task.FromResult((User)null));
            var authService = new AuthService(mockRepo.Object);
            var user = await authService.Login("admin", "password");
            Assert.Null(user);
        }
        
        [Fact]
        public async void Login_BadPassword_ReturnsNull()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(mr => mr.GetByUserName(It.IsAny<string>())).Returns(Task.FromResult(new User()
            {
                UserName = "admin",
                PasswordHash = "$2a$14$dX6l/KBxB/fIDcbEoHVuveqLB7kAX87yzrxvnoD3shF/gC8Ll28J2"
            }));
            var authService = new AuthService(mockRepo.Object);
            var user = await authService.Login("admin", "badpassword");
            Assert.Null(user);
        }

        [Fact]
        public async void Login_GoodPassword_ReturnsUser()
        {
            var mockRepo = new Mock<IUserRepository>();
            var storedUser = new User()
            {
                UserName = "admin",
                PasswordHash = "$2a$14$dX6l/KBxB/fIDcbEoHVuveqLB7kAX87yzrxvnoD3shF/gC8Ll28J2"
            };
            mockRepo.Setup(mr => mr.GetByUserName(It.IsAny<string>())).Returns(Task.FromResult(storedUser));
            var authService = new AuthService(mockRepo.Object);
            var user = await authService.Login("admin", "password");
            Assert.NotNull(user);
            Assert.Equal("admin", user.UserName);
        }
    }
}
