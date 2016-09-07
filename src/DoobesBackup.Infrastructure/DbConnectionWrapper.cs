namespace DoobesBackup.Infrastructure
{
    using Dapper;
    using System;
    using System.Data;

    public class DbConnectionWrapper : IDisposable
    {
        private readonly IDbConnection innerDbConnection;
        private bool transactionOpen = false;
        private int nestLevel = 0;
        private bool isDisposed = false;

        public DbConnectionWrapper(IDbConnection dbConnection)
        {
            this.innerDbConnection = dbConnection;
        }

        public IDbConnection Connection
        {
            get
            {
                return this.innerDbConnection;
            }
        }

        public bool IsDisposed
        {
            get
            {
                return this.isDisposed;
            }
        }

        public void AddNestLevel()
        {
            if (this.isDisposed) throw new ObjectDisposedException(GetType().FullName);
            this.nestLevel++;
        }

        public void StartTransaction()
        {
            if (this.isDisposed) throw new ObjectDisposedException(GetType().FullName);
            this.innerDbConnection.Execute("BEGIN");
            this.transactionOpen = true;
        }

        public void Commit()
        {
            if (this.isDisposed) throw new ObjectDisposedException(GetType().FullName);
            this.innerDbConnection.Execute("COMMIT");
            this.transactionOpen = false;
        }

        public void Dispose()
        {
            this.nestLevel--;
            if (this.nestLevel == 0)
            {
                if (transactionOpen)
                {
                    this.innerDbConnection.Execute("ROLLBACK");
                }

                this.innerDbConnection.Dispose();
                this.isDisposed = true;
            }
        }
    }
}
