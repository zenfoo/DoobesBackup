namespace DoobesBackup.Domain
{
    using System;
    using System.Collections.Generic;

    public interface IBackupLocation
    {
        void Configure(IDictionary<string, string> configuration);
    }
}
