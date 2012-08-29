using Griffin.Logging.Targets.File;

namespace Griffin.Logging.Tests.Targets.File
{
    public class TestWriter : IFileWriter
    {
        public static readonly TestWriter Instance = new TestWriter();

        private readonly FileConfiguration _configuration = new FileConfiguration();
        private string _writtenEntry;

        public string WrittenEntry
        {
            get { return _writtenEntry; }
        }

        #region IFileWriter Members

        public FileConfiguration Configuration
        {
            get { return _configuration; }
        }

        public string Name
        {
            get { return "TestWriter"; }
        }

        public void Write(string logEntry)
        {
            _writtenEntry = logEntry;
        }

        #endregion
    }
}