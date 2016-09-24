namespace DoobesBackup.Infrastructure
{
    using Dapper;
    using System;
    using System.Data;

    public class DbConnectionWrapper : IDisposable
    {
        private IDbConnection innerDbConnection;
        private bool transactionOpen = false;
        private int nestLevel = 0;
        private int transactionOpenNestLevel = 0;
        private bool isDisposed = false;

        public DbConnectionWrapper(IDbConnection dbConnection)
        {
            this.innerDbConnection = dbConnection;
            this.nestLevel = 1;
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

        public DbConnectionWrapper AddScope()
        {
            if (this.isDisposed) throw new ObjectDisposedException(GetType().FullName);

            this.nestLevel++;

            return this;
        }

        public void StartTransaction()
        {
            if (this.isDisposed) throw new ObjectDisposedException(GetType().FullName);
            this.innerDbConnection.Execute("BEGIN");
            this.transactionOpen = true;
            this.transactionOpenNestLevel = this.nestLevel;
        }

        public void Commit()
        {
            if (this.isDisposed) throw new ObjectDisposedException(GetType().FullName);

            // Only commit if this is the scope where we started the transaction
            if (this.nestLevel == this.transactionOpenNestLevel)
            {
                this.innerDbConnection.Execute("COMMIT");
                this.transactionOpen = false;
            }
        }

        public void Dispose()
        {
            // If a tranaction was opened at this nesting level we should dispose it
            if (transactionOpen && this.transactionOpenNestLevel == this.nestLevel)
            {
                this.innerDbConnection.Execute("ROLLBACK");
                this.transactionOpen = false;
            }

            // Reduce the nesting level
            this.nestLevel--;

            // Dispose if we are at the outer nest level
            if (nestLevel <= 0)
            {
                // If the transaction is still open force a rollback
                if (transactionOpen)
                {
                    this.innerDbConnection.Execute("ROLLBACK");
                    this.transactionOpen = false;
                }

                this.innerDbConnection.Dispose();
                this.innerDbConnection = null;
                this.isDisposed = true;
            }
        }
    }
}
