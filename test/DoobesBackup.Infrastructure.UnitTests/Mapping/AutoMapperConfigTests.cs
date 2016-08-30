//-----------------------------------------------------------------------
// <copyright file="AutoMapperConfigTests.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Infrastructure.UnitTests
{
    using Infrastructure.Mapping;
    using Xunit;

    /// <summary>
    /// Tests for the AutoMapperConfig class
    /// </summary>
    public class AutoMapperConfigTests
    {
        [Fact]
        public void AutoMapperConfig_VerifyConfiguration()
        {
            var config = new AutoMapperConfig();
            config.Configure();
        }
    }
}
