using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Serialization;
using Xunit;

namespace Griffin.Logging.Net.Tests
{
    public class SerializationTests
    {
        [Fact]
        public void Json()
        {
            var serializer = new DataContractJsonSerializer(typeof (LogEntryDTO));
            var entry = CreateEntry();

            var ms = new MemoryStream();
            serializer.WriteObject(ms, entry);
            ms.Position = 0;
            var actual = (LogEntryDTO)serializer.ReadObject(ms);

            Assert.Equal(entry.Exception.ExceptionName, actual.Exception.ExceptionName);
        }


        [Fact]
        public void DataContractXml()
        {
            var serializer = new DataContractSerializer(typeof(LogEntryDTO));
            var entry = CreateEntry();

            var ms = new MemoryStream();
            serializer.WriteObject(ms, entry);
            ms.Position = 0;
            var actual = (LogEntryDTO)serializer.ReadObject(ms);

            Assert.Equal(entry.Exception.ExceptionName, actual.Exception.ExceptionName);
        }

        private static LogEntryDTO CreateEntry()
        {
            var entry = new LogEntryDTO()
                {
                    ApplicationName = "arne",
                    ComputerName = "Mamma",
                    CreatedAt = DateTime.Now,
                    Exception = new ExceptionDTO(new InvalidOperationException("SomeOp"))
                };
            return entry;
        }

        [Fact]
        public void XmlFormatter()
        {
            var serializer = new XmlSerializer(typeof (LogEntryDTO));
            var entry = CreateEntry();

            var ms = new MemoryStream();
            serializer.Serialize(ms, entry);
            ms.Position = 0;
            var actual = (LogEntryDTO)serializer.Deserialize(ms);

            Assert.Equal(entry.Exception.ExceptionName, actual.Exception.ExceptionName);
        }
    }
}
