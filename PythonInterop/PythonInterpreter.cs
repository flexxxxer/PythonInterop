namespace PythonInterop
{
    /// <summary>
    /// Represents a pair of values: the path and version of the interpreter
    /// </summary>
    public readonly struct PythonInterpreter
    {
        public PythonInterpreter(string path, string version)
        {
            Path = path;
            Version = version;
        }

        /// <summary>
        /// Full path to interpreter
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Cropped version of the interpreter (that is, only numbers with dots in the form of a separator, for example 3.8.2)
        /// </summary>
        public string Version { get; }
    }
}
