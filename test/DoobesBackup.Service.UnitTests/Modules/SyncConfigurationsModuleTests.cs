//-----------------------------------------------------------------------
// <copyright file="SyncConfigurationModuleTests.cs" company="doobes.com">
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
    using System;
    using Xunit;

    /// <summary>
    /// Tests for the BackupSource class
    /// </summary>
    public class SyncConfigurationsModuleTests : IDisposable
    {

        public SyncConfigurationsModuleTests()
        {
            DoobesBackup.Framework.GlobalConfigurator.Configure();
        }

        public void Dispose()
        {
        }

        /// <summary>
        /// Test the name property is assigned correctly
        /// </summary>
        [Fact]
        public async void Should_return_ok_with_correct_route()
        {
            var mockRepo = new Mock<ISyncConfigurationRepository>();
            var bootstrapper = new ConfigurableBootstrapper(
                config =>
                {
                    config.Module<SyncConfigurationsModule>();
                    config.Dependency(mockRepo.Object);
                });

            var browser = new Browser(bootstrapper);

            BrowserResponse response = await browser.Get("/syncconfigurations");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
