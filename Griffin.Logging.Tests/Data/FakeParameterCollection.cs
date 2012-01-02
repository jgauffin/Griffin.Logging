using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Griffin.TestTools.Data
{
    public class FakeParameterCollection : DbParameterCollection
    {
        public IDbCommand DbCommand { get; set; }
        private List<DbParameter> _parameters = new List<DbParameter>();

        public FakeParameterCollection(IDbCommand dbCommand)
        {
            DbCommand = dbCommand;
        }

        public FakeParameterCollection(FakeParameterCollection parameters)
        {
            foreach (var parameter in parameters)
            {
                _parameters.Add((DbParameter)parameter);
            }
        }

        public override IEnumerator GetEnumerator()
        {
            return _parameters.GetEnumerator();
        }

        protected override DbParameter GetParameter(int index)
        {
            return _parameters[index];
        }

        protected override DbParameter GetParameter(string parameterName)
        {
            return _parameters.First(p => p.ParameterName == parameterName);
        }

        public override void CopyTo(Array array, int index)
        {
            if (array == null) throw new ArgumentNullException("array");
            for (int i = 0; i < _parameters.Count; i++)
            {
                array.SetValue(_parameters[i], index + i);
            }
        }

        protected override void SetParameter(string parameterName, DbParameter value)
        {
            throw new NotImplementedException();
        }

        public override int Count { get { return _parameters.Count; } }
        public override object SyncRoot { get { return _parameters; } }
        public override bool IsSynchronized { get { return false; } }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.IList"/>.
        /// </summary>
        /// <param name="value">The object to add to the <see cref="T:System.Collections.IList"/>.</param>
        /// <returns>
        /// The position into which the new element was inserted, or -1 to indicate that the item was not inserted into the collection,
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.IList"/> is read-only.-or- The <see cref="T:System.Collections.IList"/> has a fixed size. </exception>
        public override int Add(object value)
        {
            _parameters.Add((DbParameter)value);
            return _parameters.Count - 1;
        }

        public override void AddRange(Array values)
        {
            foreach (var value in values)
            {
                _parameters.Add((DbParameter)value);
            }
        }

        public override bool Contains(object value)
        {
            return _parameters.Contains(value);
        }

        public override void Clear()
        {
            _parameters.Clear();
        }

        public override int IndexOf(object value)
        {
            return _parameters.IndexOf((DbParameter)value);
        }

        public override void Insert(int index, object value)
        {
            _parameters.Insert(index, (DbParameter)value);
        }

        public override void Remove(object value)
        {
            _parameters.Remove((DbParameter)value);
        }

        public override void RemoveAt(int index)
        {
            _parameters.RemoveAt(index);
        }



        public override bool IsReadOnly { get { return false; } }
        public override bool IsFixedSize { get { return false; } }
        public override bool Contains(string parameterName)
        {
            return _parameters.Any(p => string.Equals(p.ParameterName, parameterName, StringComparison.OrdinalIgnoreCase));
        }

        public override int IndexOf(string parameterName)
        {
            return _parameters.FindIndex(p => p.ParameterName == parameterName);
        }

        public override void RemoveAt(string parameterName)
        {
            _parameters.RemoveAll(p => p.ParameterName == parameterName);
        }

        protected override void SetParameter(int index, DbParameter value)
        {
            _parameters[index] = value;
        }
    }
}