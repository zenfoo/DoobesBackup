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
        private readonly IList<IBackupDestination> backupDestinations;

        public BackupDestinationRepository()
        {
            // Locate all the backup destinations in this assembly
            this.backupDestinations = new List<IBackupDestination>();
            var assembly = typeof(BackupDestinationRepository).GetTypeInfo().Assembly;
            foreach (var type in assembly.GetTypes())
            {
                if (typeof(IBackupDestination).IsAssignableFrom(type))
                {
                    var backupLocation = (IBackupDestination)Activator.CreateInstance(type);
                    this.backupDestinations.Add(backupLocation);
                }
            }
        }

        public bool Create(IBackupDestination entity)
        {
            throw new NotImplementedException();
        }
        
        public void Delete(IBackupDestination entity)
        {
            throw new NotImplementedException();
        }

        public IBackupDestination Get(Guid id)
        {
            return this.backupDestinations.Where(bd => bd.Id == id).Single();
        }

        public IEnumerable<IBackupDestination> GetAll()
        {
            return this.backupDestinations;
        }

        public bool Update(IBackupDestination entity)
        {
            throw new NotImplementedException();
        }
    }
}
