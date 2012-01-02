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

    // get a logger for a class
    var logger = LogManager.GetLogger<MyClass>();

	// Log an exception. The inner exceptions are logged too (recursive)
    logger.Warning("Ooops, we got an exception.", ex);

	// Built in formatting for cleaner syntax.
    logger.Info("Welcome {0}!.", user.Name);


Installation
------------

Use nuget: "Install-package griffin.logging"


Extending
---------

* Create a custom logmanager by implementing ILogManager and assign it using LogManager.Assign
* Create a custom target by implementing ILogTarget. Look at https://github.com/jgauffin/Griffin.Logging/blob/master/Griffin.Logging/Targets/AdoNetTarget.cs for an example
* Add a IPreFilter or a IPostFilter to filter log entries.
* Create extension methods for one of the FluentXXX classes to inject your custom classes to the fluent configuration.
