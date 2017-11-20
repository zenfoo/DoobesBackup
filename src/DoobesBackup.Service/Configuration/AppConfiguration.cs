//-----------------------------------------------------------------------
// <copyright file="AppConfiguration.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Service.Configuration
{
    /// <summary>
    /// General service configuration
    /// </summary>
    public class AppConfiguration
    {
        /// <summary>
        /// Gets or sets the smtp configuration
        /// </summary>
        public SmtpSettings Smtp { get; set; }

        public AuthSettings Auth { get; set; }
    }
}
