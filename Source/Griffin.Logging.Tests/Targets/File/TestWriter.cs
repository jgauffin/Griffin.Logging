using Griffin.Logging.Targets.File;

namespace Griffin.Logging.Tests.Targets.File
{
    public class TestWriter : IFileWriter
    {
        public static readonly TestWriter Instance = new TestWriter();

        FileConfiguration _configuration = new FileConfiguration();
        private string _writtenEntry;

        public FileConfiguration Configuration
        {
            get { return _configuration; }
        }

        public string Name
        {
            get { return "TestWriter"; }
        }

        public string WrittenEntry
        {
            get { return _writtenEntry; }
        }

        public void Write(string logEntry)
        {
            _writtenEntry = logEntry;
        }
    }
}