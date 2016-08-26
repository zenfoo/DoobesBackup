namespace DoobesBackup.Infrastructure
{
    using Domain;
    using Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class BackupDestinationRepository : IBackupDestinationRepository
    {
        private readonly List<BackupDestination> backupDestinations;

        public BackupDestinationRepository()
        {
            // Locate all the backup destinations in this assembly
            this.backupDestinations = new List<BackupDestination>();
            var assembly = typeof(BackupDestinationRepository).GetTypeInfo().Assembly;
            foreach (var type in assembly.GetTypes())
            {
                if (typeof(BackupDestination).IsAssignableFrom(type))
                {
                    var backupLocation = (BackupDestination)Activator.CreateInstance(type);
                    this.backupDestinations.Add(backupLocation);
                }
            }
        }

        public bool Save(BackupDestination entity)
        {
            throw new NotImplementedException();
        }
        
        public bool Delete(BackupDestination entity)
        {
            throw new NotImplementedException();
        }

        public BackupDestination Get(Guid id)
        {
            return this.backupDestinations.Where(bd => bd.Id == id).Single();
        }

        public IEnumerable<BackupDestination> GetAll()
        {
            return this.backupDestinations;
        }
    }
}
