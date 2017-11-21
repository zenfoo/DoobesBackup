namespace DoobesBackup.Service.UnitTests.Configuration
{
    using DoobesBackup.Service.Configuration;
    using DoobesBackup.Service.Modules;
    using Nancy;
    using System.Linq;
    using Xunit;

    public class ContainerConfigTests
    {
        [Fact]
        public void GetContainer_VerifysOk()
        {
            var allTypes = typeof(AuthModule).Assembly.GetTypes();
            var nancyModules = allTypes
                .Where(t => typeof(NancyModule).IsAssignableFrom(t));
            var container = ContainerConfig.GetContainer(new AppConfiguration(), nancyModules);
            container.Verify();
        }
    }
}
