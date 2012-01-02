using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Griffin.TestTools.Data
{
    public class FakeCommand : DbCommand
    {
        private readonly List<string> _commandStrings = new List<string>();
        private readonly FakeParameterCollection _parameters;
        private DbConnection _connection;
        private List<FakeParameterCollection> _parameterCollections = new List<FakeParameterCollection>();

        public FakeCommand(DbConnection fakeDbConnection, DataTable nextResult)
        {
            _parameters = new FakeParameterCollection(this);
            NextResult = nextResult;
            _connection = fakeDbConnection;
        }

        public List<string> CommandStrings
        {
            get { return _commandStrings; }
        }


        public override string CommandText { get; set; }
        public override int CommandTimeout { get; set; }
        public override CommandType CommandType { get; set; }

        protected override DbConnection DbConnection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        protected override DbParameterCollection DbParameterCollection
        {
            get { return _parameters; }
        }

        protected override DbTransaction DbTransaction
        {
            get { return Transaction; }
            set { Transaction = value; }
        }

        public override bool DesignTimeVisible { get; set; }
        public bool ExecuteNonQueryInvoked { get; set; }
        public int ExecuteNonQueryResult { get; set; }
        public IDataReader ExecuteReaderResult { get; set; }
        public bool ExecuteScalarInvoked { get; set; }

        public object ExecuteScalarResult { get; set; }

        public List<FakeParameterCollection> ExecutedParameterCollections
        {
            get { return _parameterCollections; }
            set { _parameterCollections = value; }
        }

        public bool IsCancelled { get; set; }
        public bool IsPrepared { get; set; }
        public DataTable NextResult { get; set; }
        public override UpdateRowSource UpdatedRowSource { get; set; }

        public override void Cancel()
        {
            IsCancelled = true;
        }

        protected override DbParameter CreateDbParameter()
        {
            return new FakeParameter();
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            CommandStrings.Add(CommandText);
            ExecutedParameterCollections.Add(new FakeParameterCollection( _parameters));
            return new FakeDataReader(NextResult.Clone());
        }

        public override int ExecuteNonQuery()
        {
            ExecutedParameterCollections.Add(new FakeParameterCollection( _parameters));
            CommandStrings.Add(CommandText);
            ExecuteNonQueryInvoked = true;
            return ExecuteNonQueryResult;
        }

        public new IDataReader ExecuteReader()
        {
            CommandStrings.Add(CommandText);
            ExecutedParameterCollections.Add(new FakeParameterCollection( _parameters));
            return ExecuteReaderResult;
        }

        public new IDataReader ExecuteReader(CommandBehavior behavior)
        {
            CommandStrings.Add(CommandText);
            ExecutedParameterCollections.Add(new FakeParameterCollection( _parameters));
            return ExecuteReaderResult;
        }

        public override object ExecuteScalar()
        {
            CommandStrings.Add(CommandText);
            ExecutedParameterCollections.Add(new FakeParameterCollection( _parameters));
            ExecuteScalarInvoked = true;
            return ExecuteScalarResult;
        }

        public override void Prepare()
        {
            IsPrepared = true;
        }

        public virtual void Reset()
        {
            IsPrepared = false;
            IsCancelled = false;
            ExecuteNonQueryInvoked = false;
            ExecuteScalarInvoked = false;
        }
    }
}