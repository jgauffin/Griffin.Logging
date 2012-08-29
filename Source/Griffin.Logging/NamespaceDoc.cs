using System.Runtime.CompilerServices;

namespace Griffin.Logging
{
    /// <summary>
    /// <para>
    /// Welcome to the documentation for the Griffin Logging framework. 
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>Select your preference</para>
    /// <list type="table">
    /// <item>
    /// <term>Quickstart</term>
    /// <definition><see cref="SimpleLogManager"/> helps you getting everything up and running by configure the logging framework with one line of code.</definition>
    /// </item>
    /// <item>
    /// <term>Fluent configuration</term>
    /// <definition>
    /// <see cref="FluentConfiguration"/> gives you a powerful and hopefully easy way to configure the logger and using all of it's magic.
    /// </definition>
    /// </item>
    /// <item>
    /// <term>Extension points</term>
    /// <definition></definition>
    /// </item>
    /// </list>
    /// </remarks>
    /// <example>
    /// Configure fluently:
    /// <code>
    /// new FluentConfiguration()
    ///   .LogNamespace("Fadd.Test").AndChildNamespaces.ToTargetNamed("Console")
    ///   .LogNamespace("System").AndChildNamespaces.ToTargetNamed("DefaultFile")
    ///   .AddTarget("Console")
    ///       .As.ConsoleLogger().Filter.OnLogLevelBetween(LogLevel.Info, LogLevel.Error)
    ///       .Done 
    ///   .AddTarget("DefaultFile")
    ///       .As.FileLogger("ErrorsOnly").Filter.OnLogLevel(LogLevel.Error)
    ///       .As.FileLogger("Everything")
    ///       .Done
    ///   .Build();
    /// 
    /// </code>
    /// </example>
    /// <example>
    /// Easy configuration:
    /// <code>
    /// SimpleLogManager.Instance.AddFile(@"D:\LogFiles\MyLog.log");
    /// </code>
    /// </example>
    /// <example>
    /// Usage:
    /// <code>
    /// <![CDATA[
    /// public class MyClass
    /// {
    ///     ILogger logger = LogManager.GetLogger<MyClass>();
    ///  
    ///     public MyClass()
    ///     {
    ///         // Built in formatting for cleaner syntax.
    ///         logger.Info("Welcome {0}!.", user.Name);
    ///     }
    ///     public void HideError()
    ///     {
    ///         try
    ///         {
    ///             throw new InvalidOperationException("No no", new OutOfScopeException("Can't thing anymore"));
    ///         }
    ///         catch (Exception err)
    ///         {
    ///             logger.Warning("Ooops, we got an exception.", ex);
    ///         }
    ///     }
    ///  
    /// }
    /// ]]>
    /// </code>
    /// </example>
    [CompilerGenerated]
    internal class NamespaceDoc
    {
    }
}