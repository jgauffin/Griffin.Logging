using System.Data;
using System.Data.Common;

namespace Griffin.TestTools.Data
{
    public class FakeTransaction : DbTransaction
    {
        private readonly DbConnection _connection;
        private readonly IsolationLevel _isolationLevel;

        public FakeTransaction(DbConnection connection)
        {
            _connection = connection;
        }

        public FakeTransaction(DbConnection connection, IsolationLevel il)
        {
            _connection = connection;
            _isolationLevel = il;
        }

        public bool IsCommitted { get; set; }

        public bool IsRolledBack { get; set; }

        protected override DbConnection DbConnection
        {
            get { return _connection; }
        }

        public override IsolationLevel IsolationLevel
        {
            get { return _isolationLevel; }
        }

        public new void Dispose()
        {
            Reset();
        }

        public virtual void Reset()
        {
            IsCommitted = false;
            IsRolledBack = false;
        }

        public override void Commit()
        {
            IsCommitted = true;
        }

        public override void Rollback()
        {
            IsRolledBack = true;
        }
    }
}