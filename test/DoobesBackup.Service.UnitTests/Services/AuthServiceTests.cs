namespace DoobesBackup.Domain.UnitTests.Services
{
    using DoobesBackup.Infrastructure.Repositories;
    using DoobesBackup.Service.Configuration;
    using DoobesBackup.Service.Services;
    using Infrastructure;
    using Moq;
    using Nancy;
    using Nancy.Testing;
    using Service.Modules;
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
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
            var authService = new AuthService(new AppConfiguration(), mockRepo.Object);
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
            var authService = new AuthService(new AppConfiguration(), mockRepo.Object);
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
            var authService = new AuthService(new AppConfiguration(), mockRepo.Object);
            var user = await authService.Login("admin", "password");
            Assert.NotNull(user);
            Assert.Equal("admin", user.UserName);
        }

        [Fact]
        public void GenerateUserToken_Succeeds()
        {
            var mockRepo = new Mock<IUserRepository>();
            var storedUser = new User()
            {
                UserName = "admin",
                PasswordHash = "$2a$14$dX6l/KBxB/fIDcbEoHVuveqLB7kAX87yzrxvnoD3shF/gC8Ll28J2"
            };
            mockRepo.Setup(mr => mr.GetByUserName(It.IsAny<string>())).Returns(Task.FromResult(storedUser));
            var authService = new AuthService(
                new AppConfiguration()
                {
                    Auth = new AuthSettings()
                    {
                        SigningSecret = "some secret must be greater than 128 bitss"
                    }
                },
                mockRepo.Object);
            var token = authService.GenerateUserToken(storedUser);
            Assert.True(!string.IsNullOrEmpty(token));
        }

        [Fact]
        public void ValidateUserToken_BadToken_ReturnsFalse()
        {
            var badToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJhZG1pbiIsInJvbGUiOiJBZG1pbmlzdHJhdG9yIiwibmJmIjoxNTExMjQwMjQ3LCJleHAiOjE1MTEyNDM4NDcsImlhdCI6MTUxMTI0MDI0NywiaXNzIjoiaHR0cDovL3Rva2VuaXNzdWVyLmRvb2Jlcy5jb20iLCJhdWQiOiJodHRwOi8vYmFja3VwLmRvb2Jlcy5jb20ifQ.IkSqGomYQlPMqmDh1ngDSJRC0D4IFP7y8NZDn__9XOM";
            var mockRepo = new Mock<IUserRepository>();
            var authService = new AuthService(
                new AppConfiguration()
                {
                    Auth = new AuthSettings()
                    {
                        SigningSecret = "some secret must be greater than 128 bits"
                    }
                },
                mockRepo.Object);
            IEnumerable<Claim> claims;
            var result = authService.ValidateUserToken(badToken, out claims);
            Assert.False(result);
        }

        [Fact]
        public void ValidateUserToken_GoodToken_ReturnsTrue()
        {
            var goodToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJhZG1pbiIsInJvbGUiOiJBZG1pbmlzdHJhdG9yIiwibmJmIjoxNTExMjM5NTQwLCJleHAiOjE1MTEyNDMxNDAsImlhdCI6MTUxMTIzOTU0MCwiaXNzIjoiaHR0cDovL3Rva2VuaXNzdWVyLmRvb2Jlcy5jb20iLCJhdWQiOiJodHRwOi8vYmFja3VwLmRvb2Jlcy5jb20ifQ.IsyerS451Y6nYwPrkNsc-Kir3o8AbO1fjO9Bn0J2RrM";
            var mockRepo = new Mock<IUserRepository>();
            var authService = new AuthService(
                new AppConfiguration()
                {
                    Auth = new AuthSettings()
                    {
                        SigningSecret = "some secret must be greater than 128 bits"
                    }
                },
                mockRepo.Object);
            IEnumerable<Claim> claims;
            var result = authService.ValidateUserToken(goodToken, out claims);
            Assert.True(result);
        }
    }
}
