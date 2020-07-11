using System;
using System.Collections.Generic;
using System.Text;

namespace PythonInterop
{
    public class PythonEngineException : Exception
    {
        public PythonEngineException(string message) : base(message) { }
    }
}
