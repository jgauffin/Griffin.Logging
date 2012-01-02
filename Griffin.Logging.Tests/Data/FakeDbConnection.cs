using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Griffin.TestTools.Data
{
    public class FakeDbConnection : DbConnection
    {
        private List<FakeCommand> _commands = new List<FakeCommand>();

        public DataTable NextResult { get; set; }

        public FakeDbConnection(DataTable nextResult)
        {
            NextResult = nextResult;
        }

        public new void Dispose()
        {
            Reset();
            base.Dispose();
        }


        public virtual void Reset()
        {
            StateReturned = ConnectionState.Closed;
            Commands.Clear();
            
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            return new FakeTransaction(this, isolationLevel);
        }

        public new IDbTransaction BeginTransaction()
        {
            return new FakeTransaction(this);
        }

        public new IDbTransaction BeginTransaction(IsolationLevel il)
        {
            return new FakeTransaction(this, il);
        }

        public override void Close()
        {
            Reset();
        }


        public override void ChangeDatabase(string databaseName)
        {
            CurrentDatabase = databaseName;
        }

        public string CurrentDatabase { get; set; }

        public new IDbCommand CreateCommand()
        {
            var cmd= new FakeCommand(this, NextResult);
            Commands.Add(cmd);
            return cmd;
        }


        protected override DbCommand CreateDbCommand()
        {
            var cmd = new FakeCommand(this, NextResult);
            Commands.Add(cmd);
            return cmd;
        }

        public override void Open()
        {
            StateReturned = ConnectionState.Open;
        }

        public override string ConnectionString { get; set; }

        public string DatabaseReturned { get; set; }

        public override string Database { get { return DatabaseReturned; } }

        public override string DataSource
        {
            get { return "FakeProvider"; }
        }

        public override string ServerVersion
        {
            get { return "40"; }
        }

        public override ConnectionState State { get { return StateReturned; } }

        public ConnectionState StateReturned { get; set; }

        public List<FakeCommand> Commands
        {
            get { return _commands; }
        }
    }
}
