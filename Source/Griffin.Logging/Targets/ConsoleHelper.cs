using System.Runtime.InteropServices;

namespace Griffin.Logging.Targets
{
    /// <summary>
    /// Utility class used to detect if the running application has an attached console window.
    /// </summary>
    /// <remarks>This helper also exists in the Core assembly. The motivation to put it in this
    /// assembly is to be able to remove a dependency to the core assembly. Hence we also make it
    /// internal to not create any confusion for the framework users.</remarks>
    internal static class ConsoleHelper
    {
        private const uint ATTACH_PARENT_PROCESS = 0x0ffffffff;
        private const int ERROR_ACCESS_DENIED = 5;
        private const int ERROR_INVALID_HANDLE = 6;

        /// <summary>
        /// Gets if the current process has a console window.
        /// </summary>
        public static bool HasConsole
        {
            get
            {
                if (AttachConsole(ATTACH_PARENT_PROCESS))
                {
                    FreeConsole();
                    return false;
                }

                //If the calling process is already attached to a console, 
                // the error code returned is ERROR_ACCESS_DENIED
                return Marshal.GetLastWin32Error() == ERROR_ACCESS_DENIED;
            }
        }

        /// <summary>
        /// allocates a new console for the calling process.
        /// </summary>
        /// <returns>If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. 
        /// To get extended error information, call Marshal.GetLastWin32Error.</returns>
        [DllImport("kernel32", SetLastError = true)]
        private static extern bool AllocConsole();

        /// <summary>
        /// Attaches the calling process to the console of the specified process.
        /// </summary>
        /// <param name="dwProcessId">[in] Identifier of the process, usually will be ATTACH_PARENT_PROCESS</param>
        /// <returns>If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. 
        /// To get extended error information, call Marshal.GetLastWin32Error.</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AttachConsole(uint dwProcessId);

        /// <summary>
        /// Detaches the calling process from its console
        /// </summary>
        /// <returns>If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. 
        /// To get extended error information, call Marshal.GetLastWin32Error.</returns>
        [DllImport("kernel32", SetLastError = true)]
        private static extern bool FreeConsole();

        /// <summary>
        /// Create a console window
        /// </summary>
        public static void CreateConsole()
        {
            AllocConsole();
        }
    }
}