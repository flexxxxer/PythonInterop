namespace PythonInterop
{
    public readonly struct PythonInterpreter
    {
        public PythonInterpreter(string path, string version)
        {
            Path = path;
            Version = version;
        }

        public string Path { get; }
        public string Version { get; }
    }
}
