using System;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace Griffin.TestTools.Data
{
    public class FakeDataReader : DbDataReader
    {
        private readonly DataTable _table;
        public bool IsClosedResult { get; set; }
        private int _rowNumber;

        public FakeDataReader(DataTable table)
        {
            _table = table;
        }

        public override void Close()
        {
            IsClosedResult = true;
        }

        public override DataTable GetSchemaTable()
        {
            return SchemaTable;
        }

        protected DataTable SchemaTable { get; set; }

        public override bool NextResult()
        {
            return _table.Rows.Count > _rowNumber;
        }

        public override bool Read()
        {
            ++_rowNumber;
            return _table.Rows.Count > _rowNumber;
        }

        /// <summary>
        /// Gets a value indicating the depth of nesting for the current row.
        /// </summary>
        /// <returns>The depth of nesting for the current row.</returns>
        public override int Depth
        {
            get { return 0; }
        }

        public override bool IsClosed
        {
            get { return IsClosedResult; }
        }

        public override int RecordsAffected
        {
            get { return _table.Rows.Count; }
        }

        public override bool GetBoolean(int ordinal)
        {
            return (bool) _table.Rows[_rowNumber][ordinal];
        }

        public override byte GetByte(int ordinal)
        {
            return (byte)_table.Rows[_rowNumber][ordinal];
        }

        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            return (byte)_table.Rows[_rowNumber][ordinal];
        }

        public override char GetChar(int ordinal)
        {
            return (char)_table.Rows[_rowNumber][ordinal];
        }

        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            return (byte)_table.Rows[_rowNumber][ordinal];
        }

        public override Guid GetGuid(int ordinal)
        {
            return (Guid)_table.Rows[_rowNumber][ordinal];
        }

        public override short GetInt16(int ordinal)
        {
            return (short)_table.Rows[_rowNumber][ordinal];
        }

        public override int GetInt32(int ordinal)
        {
            return (int)_table.Rows[_rowNumber][ordinal];
        }

        public override long GetInt64(int ordinal)
        {
            return (long)_table.Rows[_rowNumber][ordinal];
        }

        public override DateTime GetDateTime(int ordinal)
        {
            return (DateTime)_table.Rows[_rowNumber][ordinal];
        }

        public override string GetString(int ordinal)
        {
            return (string)_table.Rows[_rowNumber][ordinal];
        }

        public override object GetValue(int ordinal)
        {
            return _table.Rows[_rowNumber][ordinal];
        }

        /// <summary>
        /// Populates an array of objects with the column values of the current row.
        /// </summary>
        /// <param name="values">An array of <see cref="T:System.Object"/> into which to copy the attribute columns.</param>
        /// <returns>
        /// The number of instances of <see cref="T:System.Object"/> in the array.
        /// </returns>
        public override int GetValues(object[] values)
        {
            for (int i = 0; i < _table.Columns.Count; i++)
            {
                values[i] = _table.Rows[_rowNumber][i];
            }

            return _table.Columns.Count;
        }

        public override bool IsDBNull(int ordinal)
        {
            return _table.Rows[_rowNumber][ordinal] is DBNull || _table.Rows[_rowNumber][ordinal] == null;
        }

        public override int FieldCount
        {
            get { return _table.Columns.Count; }
        }

        public override object this[int ordinal]
        {
            get { return _table.Rows[_rowNumber][ordinal]; }
        }

        public override object this[string name]
        {
            get
            {
                return (byte)_table.Rows[_rowNumber][GetOrdinal(name)];
            }
        }

        public override bool HasRows
        {
            get { return _table.Rows.Count>0; }
        }

        public override decimal GetDecimal(int ordinal)
        {
            return (decimal)_table.Rows[_rowNumber][ordinal];
        }

        public override double GetDouble(int ordinal)
        {
            return (double)_table.Rows[_rowNumber][ordinal];
        }

        public override float GetFloat(int ordinal)
        {
            return (float)_table.Rows[_rowNumber][ordinal];
        }

        public override string GetName(int ordinal)
        {
            return _table.Columns[ordinal].ColumnName;
        }

        public override int GetOrdinal(string name)
        {
            for (int i = 0; i < _table.Columns.Count; i++)
            {
                if (_table.Columns[i].ColumnName == name)
                    return i;
            }

            throw new IndexOutOfRangeException(name + " was not found.");
        }

        public override string GetDataTypeName(int ordinal)
        {
            return _table.Rows[_rowNumber][ordinal].GetType().Name;
        }

        public override Type GetFieldType(int ordinal)
        {
            return _table.Rows[_rowNumber][ordinal].GetType();
        }

        public override IEnumerator GetEnumerator()
        {
            return _table.Rows.GetEnumerator();
        }
    }
}