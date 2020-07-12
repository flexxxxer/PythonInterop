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
        /// <param name="stdOut">All data from StdOut</param>
        /// <param name="stdError">All data from StdError</param>
        /// <param name="exitCode">Script exit code</param>
        public ExecutionResult(string stdOut, string stdError, int exitCode)
        {
            StdOut = stdOut;
            StdError = stdError;
            ExitCode = exitCode;
        }

        /// <summary>
        /// Script StdOut data
        /// </summary>
        public string StdOut { get; }

        /// <summary>
        /// Script StdError data
        /// </summary>
        public string StdError { get; }

        /// <summary>
        /// Script exit code
        /// </summary>
        public int ExitCode { get; }
    }
}
