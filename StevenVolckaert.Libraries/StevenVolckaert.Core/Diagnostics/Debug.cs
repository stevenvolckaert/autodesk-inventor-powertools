using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace StevenVolckaert.Diagnostics
{
    /// <summary>
    /// Provides a set of methods and properties that help debug your code.
    /// This class cannot be inherited.
    /// </summary>
    public static class Debug
    {
        private const string DateTimeStringFormat = "T";

        /// <summary>
        /// Writes a specified message followed by a line terminator to the debugger
        /// by using the OutputDebugString function.
        /// </summary>
        /// <param name="callerMemberName">The name of the member that's calling this method.</param>
        /// <param name="message">The message to write to the debugger.</param>
        [Conditional("DEBUG")]
        public static void WriteLine(string callerMemberName, string message)
        {
            System.Diagnostics.Debug.WriteLine("[{0}] Exception in {1}: {2}", DateTime.Now.ToString(DateTimeStringFormat, CultureInfo.CurrentCulture), callerMemberName, message);
        }

        /// <summary>
        /// Writes a specified message followed by a line terminator to the debugger
        /// by using the OutputDebugString function.
        /// </summary>
        /// <param name="callerMemberInfo">Metadata of the member that called this method.</param>
        /// <param name="message">The message to write to the debugger.</param>
        /// <exception cref="ArgumentNullException"><paramref name="callerMemberInfo"/> is <c>null</c>.</exception>
        [Conditional("DEBUG")]
        public static void WriteLine(MemberInfo callerMemberInfo, string message)
        {
            if (callerMemberInfo == null)
                throw new ArgumentNullException("callerMemberInfo");

            System.Diagnostics.Debug.WriteLine("[{0}] {1}.{2}: {3}", DateTime.Now.ToString(DateTimeStringFormat, CultureInfo.CurrentCulture), callerMemberInfo.DeclaringType.FullName, callerMemberInfo.Name, message);
        }

        /// <summary>
        /// Writes a formatted string followed by a line terminator to the debugger by
        /// using the OutputDebugString function.
        /// </summary>
        /// <param name="callerMemberInfo">Metadata of the member that called this method.</param>
        /// <param name="format">A composite format string that contains text intermixed with zero or more
        /// format items, which correspond to objects in the args array.</param>
        /// <param name="args">An object array containing zero or more objects to format.</param>
        /// <exception cref="ArgumentNullException"><paramref name="callerMemberInfo"/> is <c>null</c>.</exception>
        [Conditional("DEBUG")]
        public static void WriteLine(MemberInfo callerMemberInfo, string format, params object[] args)
        {
            if (callerMemberInfo == null)
                throw new ArgumentNullException("callerMemberInfo");

            WriteLine(callerMemberInfo, string.Format(CultureInfo.InvariantCulture, format, args));
        }

        /// <summary>
        /// Writes a formatted string followed by a line terminator to the debugger by
        /// using the OutputDebugString function.
        /// </summary>
        /// <param name="callerMemberInfo">Metadata of the member that called this method.</param>
        /// <param name="exception">The exception object that must be written to the debugger.</param>
        /// <exception cref="ArgumentNullException"><paramref name="callerMemberInfo"/> or <paramref name="exception"/> is <c>null</c>.</exception>
        [Conditional("DEBUG")]
        public static void WriteLine(MemberInfo callerMemberInfo, Exception exception)
        {
            if (callerMemberInfo == null)
                throw new ArgumentNullException("callerMemberInfo");

            if (exception == null)
                throw new ArgumentNullException("exception");

            System.Diagnostics.Debug.WriteLine("[{0}] Exception in {1}.{2}: {3}", DateTime.Now.ToString(DateTimeStringFormat, CultureInfo.CurrentCulture), callerMemberInfo.DeclaringType.FullName, callerMemberInfo.Name, exception.Message);
        }

        /// <summary>
        /// Writes a formatted string followed by a line terminator to the debugger by
        /// using the OutputDebugString function.
        /// </summary>
        /// <param name="callerMemberInfo">Metadata of the member that called this method.</param>
        /// <param name="exception">The exception object that must be written to the debugger.</param>
        /// <param name="message">The message to write to the debugger.</param>
        /// <exception cref="ArgumentNullException"><paramref name="callerMemberInfo"/> or <paramref name="exception"/> is <c>null</c>.</exception>
        [Conditional("DEBUG")]
        public static void WriteLine(MemberInfo callerMemberInfo, Exception exception, string message)
        {
            if (callerMemberInfo == null)
                throw new ArgumentNullException("callerMemberInfo");

            if (exception == null)
                throw new ArgumentNullException("exception");

            if (String.IsNullOrWhiteSpace(message))
                WriteLine(callerMemberInfo, exception);
            else
            {
                System.Diagnostics.Debug.WriteLine("[{0}] Exception in {1}.{2}: {3}", DateTime.Now.ToString(DateTimeStringFormat, CultureInfo.CurrentCulture), callerMemberInfo.DeclaringType.FullName, callerMemberInfo.Name, message);
                System.Diagnostics.Debug.WriteLine(exception.Message);
            }
        }
    }
}
