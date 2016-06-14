namespace DoobesBackup.Infrastructure
{
    using DoobesBackup.Domain;
    using System;
    using System.Collections.Generic;
    public class FileSystemLocation : IBackupDestination, IBackupSource
    {
        private static readonly Guid id = new Guid("5BD0267D-158E-4996-9C83-89DC408B32B3");
        
        public Guid? Id
        {
            get
            {
                return FileSystemLocation.id;
            }
        }

        public string Name
        {
            get
            {
                return "File System";
            }
        }

        public void Configure(IDictionary<string, string> configuration)
        {

        }
    }
}
