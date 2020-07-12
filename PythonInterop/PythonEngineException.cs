using System;

namespace PythonInterop
{
    public class PythonEngineException : Exception
    {
        /// <summary>
        /// Init PythonEngineException
        /// </summary>
        /// <param name="message">What is happen</param>
        public PythonEngineException(string message) : base(message) { }
    }
}
