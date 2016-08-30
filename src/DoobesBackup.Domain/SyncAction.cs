namespace DoobesBackup.Domain
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Describes an action that should be taken during a sync process
    /// </summary>
    public class SyncAction
    {

        public BackupSource Source { get; private set; }

        public BackupDestination Destination { get; private set; }

        public SyncActionType Action { get; private set; }

        public File SourceFile { get; private set; }

        public File DestinationFile { get; private set; }

        public SyncAction(
            BackupSource source, 
            BackupDestination destination, 
            SyncActionType actionType,
            File sourceFile,
            File destinationFile)
        {
            this.Source = source;
            this.Destination = destination;
            this.Action = actionType;
            this.SourceFile = sourceFile;
            this.DestinationFile = destinationFile;
        }
    }
}
