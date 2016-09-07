//-----------------------------------------------------------------------
// <copyright file="GlobalAutoMapperConfigTests.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Service.UnitTests
{
    using Framework;
    using Xunit;

    /// <summary>
    /// Tests for the AutoMapperConfig class
    /// </summary>
    public class GlobalAutoMapperConfigTests
    {
        [Fact]
        public void AutoMapperConfig_VerifyConfiguration()
        {
            var config = new GlobalAutoMapperConfig();
            config.Configure();
        }
    }
}
