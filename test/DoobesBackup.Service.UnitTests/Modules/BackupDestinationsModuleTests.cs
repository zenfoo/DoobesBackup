//-----------------------------------------------------------------------
// <copyright file="BackupDestinationsModuleTests.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Domain.UnitTests
{
    using Infrastructure;
    using Moq;
    using Nancy;
    using Nancy.Testing;
    using Service.Modules;
    using Xunit;

    /// <summary>
    /// Tests for the BackupDestinations class
    /// </summary>
    public class BackupDestinationsModuleTests
    {
        /// <summary>
        /// Test the name property is assigned correctly
        /// </summary>
        [Fact]
        public async void Should_return_ok_with_correct_route()
        {
            var mockRepo = new Mock<IBackupDestinationRepository>();
            var bootstrapper = new ConfigurableBootstrapper(
                config =>
                {
                    config.Module<BackupDestinationsModule>();
                    config.Dependency(mockRepo.Object);
                });

            var browser = new Browser(bootstrapper);

            BrowserResponse response = await browser.Get("/backupdestinations");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
