using System;
using System.Collections.Generic;
using System.Text;

namespace PythonInterop
{
    public readonly struct ExecutionResult
    {
        public ExecutionResult(string stdOut, string stdError, int exitCode)
        {
            StdOut = stdOut;
            StdError = stdError;
            ExitCode = exitCode;
        }

        public string StdOut { get; }
        public string StdError { get; }
        public int ExitCode { get; }
    }
}
