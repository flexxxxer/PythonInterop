using System.IO;
using System.Text;

namespace PythonInterop
{
    /// <summary>
    /// Represents the output data after the script is executed 
    /// </summary>
    public readonly struct ExecutionResult
    {
        /// <summary>
        /// Init ExecutionResult
        /// </summary>
        /// <param name="stdOutStream">StdOut data stream</param>
        /// <param name="stdErrorStream">StdError data stream</param>
        /// <param name="exitCode">Script exit code</param>
        public ExecutionResult(MemoryStream stdOutStream, MemoryStream stdErrorStream, int exitCode)
        {
            ExitCode = exitCode;
            StdOutStream = stdOutStream;
            StdErrorStream = stdErrorStream;
        }

        public MemoryStream StdOutStream { get; }
        public MemoryStream StdErrorStream { get; }

        /// <summary>
        /// Script StdOut data
        /// </summary>
        public string StdOut => Encoding.ASCII.GetString(StdOutStream.ToArray());

        /// <summary>
        /// Script StdError data
        /// </summary>
        public string StdError => Encoding.ASCII.GetString(StdErrorStream.ToArray());

        /// <summary>
        /// Script exit code
        /// </summary>
        public int ExitCode { get; }
    }
}
