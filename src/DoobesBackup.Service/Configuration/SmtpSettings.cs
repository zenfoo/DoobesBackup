//-----------------------------------------------------------------------
// <copyright file="SmtpSettings.cs" company="doobes.com">
//     Copyright (c) doobes.com. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace DoobesBackup.Service.Configuration
{
    using DoobesBackup.Framework;

    /// <summary>
    /// Describes an Smtp configuration
    /// </summary>
    public class SmtpSettings
    {
        /// <summary>
        /// Gets or sets the smtp host server
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Gets or sets the smtp username to authenticate with
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the smtp password to authenticate with
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether smtp should be sent over an SSL channel
        /// </summary>
        public bool UseSsl { get; set; }

        /// <summary>
        /// Gets or sets the port number to reach the smtp server on
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Verify that the configuration is valid
        /// </summary>
        public void Verify()
        {
            AssertThat.IsNotNullOrEmpty(this.Server, "Smtp server was not specified!");
        }
    }
}
