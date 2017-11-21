namespace DoobesBackup.Service.Configuration
{
    using DoobesBackup.Framework;

    public class AuthSettings
    {
        /// <summary>
        /// The signing secret to use for encryption purposes
        /// </summary>
        public string SigningSecret { get; set; }   

        /// <summary>
        /// Verify that the configuration is valid
        /// </summary>
        public void Verify()
        {
            AssertThat.IsNotNullOrEmpty(this.SigningSecret, "The signing secret must be specified!");

            // TODO: verify that the signing secret is > 128 bits
        }
    }
}
