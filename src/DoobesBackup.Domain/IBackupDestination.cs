namespace DoobesBackup.Domain
{
    using System;

    public interface IBackupDestination : IAggregateRoot, IBackupLocation
    {
        string Name { get; }
    }
}
