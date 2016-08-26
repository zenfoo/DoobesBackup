using DoobesBackup.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoobesBackup.Infrastructure
{
    public interface IBackupDestinationRepository : IRepository<BackupDestination>
    {
        
    }
}
