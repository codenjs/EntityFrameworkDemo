using System;
using System.Configuration;
using System.Transactions;

namespace Tests.Framework
{
    public abstract class DbTestBase : IDisposable
    {
        protected DbTestUtilities DbTestUtilities;
        private TransactionScope _transactionScope;

        protected DbTestBase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
            DbTestUtilities = new DbTestUtilities(connectionString);

            DbTestUtilities.CreateDatabaseIfNotExists();
            InitializeTransaction();
        }

        private void InitializeTransaction()
        {
            var runTestsWithoutTransactions = bool.Parse(ConfigurationManager.AppSettings["RunTestsWithoutTransactions"]);
            if (runTestsWithoutTransactions)
                return;

            _transactionScope = CreateScope();
        }

        private TransactionScope CreateScope()
        {
            return new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.Serializable,
                    Timeout = TransactionManager.MaximumTimeout
                },
                TransactionScopeAsyncFlowOption.Enabled
            );
        }

        public void Dispose()
        {
            if (_transactionScope != null)
            {
                _transactionScope.Dispose();
                _transactionScope = null;
            }
        }
    }
}
