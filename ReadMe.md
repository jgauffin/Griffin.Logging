Griffin.Logging is a Logging framework for .NET similar to log4net and NLog.

The difference is all in the syntax.

Configuration
-------------

You can either use the simple interface to get going quickly:

      // configuration
      SimpleLogger.AddFile("D:\LogFiles\MyLog.log"); // date can automatically be appended to the log name
      SimpleLogger.AddConsole(); // A console windows is created automatically for winforms projects
    
Or the fluent syntax:

    var config = new FluentConfiguration()
      .LogNamespace("Fadd.Test").AndSubNamespaces.ToTargetNamed("Console")
      .LogNamespace("System").AndSubNamespaces.ToTargetNamed("DefaultFile")
      .AddTarget("Console")
          .As.ConsoleLogger().Filter.OnLogLevelBetween(LogLevel.Info, LogLevel.Error)
          .Done 
      .AddTarget("DefaultFile")
          .As.FileLogger("ErrorsOnly").Filter.OnLogLevel(LogLevel.Error)
          .As.FileLogger("Everything")
          .Done
      .Build();

Usage
-----

    // usage
    var logger = LogManager.GetLogger<MyClass>();
    logger.Warning("Ooops, we got an exception.", ex);
    logger.Info("Welcome {0}!.", user.Name);


Installation
------------

Use nuget: "Install-package griffin.logging"
