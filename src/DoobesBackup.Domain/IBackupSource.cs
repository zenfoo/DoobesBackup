namespace DoobesBackup.Domain
{
    using System;

    public interface IBackupSource : IAggregateRoot, IBackupLocation
    {
        string Name { get; }
    }
}
