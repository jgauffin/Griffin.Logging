using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griffin.Logging.Targets.File;

namespace Griffin.Logging.Tests.Targets.File
{
    public class FileWriterTest : FileWriter 
    {
        public FileWriterTest() : base("MyTest", new FileConfiguration())
        {
        }

        public void Test()
        {
            
        }
    }
}
