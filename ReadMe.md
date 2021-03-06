Griffin.Logging is a Logging framework for .NET similar to log4net and NLog.

The difference is all in the syntax.

Configuration
-------------

You can either use the simple interface to get going quickly:

	SimpleLogManager.Instance.AddFile(@"D:\LogFiles\MyLog.log");
	SimpleLogManager.Instance.AddConsole(false);
    
Or the fluent syntax:

	new FluentConfiguration()
		.LogNamespace("ConsoleApplication1").AndChildNamespaces.ToTargetNamed("Console")
		.LogNamespace("System").AndChildNamespaces.ToTargetNamed("DefaultFile")
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

	public class MyClass
	{
		ILogger logger = LogManager.GetLogger<MyClass>();
	 
		public MyClass()
		{
			// Built in formatting for cleaner syntax.
			logger.Info("Welcome {0}!.", user.Name);
		}
		public void HideError()
		{
			try
			{
				throw new InvalidOperationException("No no", new OutOfScopeException("Can't thing anymore"));
			}
			catch (Exception err)
			{
				// exceptions details are properly formatted.
				logger.Warning("Ooops, we got an exception.", ex);
			}
		}
	}


Installation (nuget)
--------------------

    Install-Package griffin.logging
	
You can also send all entries over MQ (great for clients)

    Install-Package griffin.logging.mq 


Extending
---------

* Create a custom logmanager by implementing [ILogManager](https://github.com/jgauffin/Griffin.Logging/blob/master/Source/Griffin.Logging/ILogManager.cs) and assign it using [LogManager.Assign].
* Create a custom target by implementing [ILogTarget](https://github.com/jgauffin/Griffin.Logging/blob/master/Source/Griffin.Logging/Targets/ILogTarget.cs). Look at [AdoNetTarget](https://github.com/jgauffin/Griffin.Logging/blob/master/Source/Griffin.Logging/Targets/AdoNetTarget.cs) for an example
* Add a [IPreFilter](https://github.com/jgauffin/Griffin.Logging/blob/master/Source/Griffin.Logging/Filters/IPreFilter.cs) or a [IPostFilter](https://github.com/jgauffin/Griffin.Logging/blob/master/Source/Griffin.Logging/Filters/IPostFilter.cs) to filter log entries.
* Create extension methods for one of the FluentXXX classes to inject your custom classes to the fluent configuration.
