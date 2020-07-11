using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PythonInterop
{
    public class PythonEngine
    {
        private static readonly Regex CheckIsPyVersion = new Regex("^Python (\\d+)\\.(\\d+)\\.(\\d+)$", RegexOptions.Singleline | RegexOptions.Compiled);

        private static OSPlatform CurrentOsPlatform
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    return OSPlatform.Linux;
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    return OSPlatform.Windows;
                }

                if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    return OSPlatform.OSX;
                }

                throw new NotSupportedException();
            }
        }

        private static Process CreateAndExecuteProcess(string file, string args)
        {
            Process ps = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = file,
                    Arguments = args,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    UseShellExecute = false
                }
            };

            ps.Start();
            ps.WaitForExit();

            return ps;
        }

        private static string GetPythonVersion(string path)
        {
            Process getVersionProcess = CreateAndExecuteProcess(path, "--version");

            string stdOut = getVersionProcess.StandardOutput.ReadToEnd().Replace(Environment.NewLine, string.Empty);

            if (CheckIsPyVersion.Match(stdOut).Success)
            {
                return stdOut.Substring(7);
            }

            string stdError = getVersionProcess.StandardError.ReadToEnd().Replace(Environment.NewLine, string.Empty);

            return CheckIsPyVersion.Match(stdError).Success ? stdError.Substring(7) : string.Empty;
        }

        public static IEnumerable<PythonInterpreter> PythonInterpreters
        {
            get
            {
                var platformsActions = new Dictionary<OSPlatform, (string, string)>()
                {
                    {OSPlatform.Windows, ("where", "python*.exe")},
                    {OSPlatform.Linux, ("find", $"{Environment.GetEnvironmentVariable("PATH")?.Replace(':', ' ')}  -executable -name python*")},
                    {OSPlatform.OSX, ("find", $"{Environment.GetEnvironmentVariable("PATH")?.Replace(':', ' ')} -perm +111 -iname python*")}
                };

                var (searcher, what) = platformsActions[CurrentOsPlatform];

                string[] paths = CreateAndExecuteProcess(searcher, what).StandardOutput.ReadToEnd().Split(Environment.NewLine.ToCharArray());

                foreach (var interpreterPath in paths.Where(p => !string.IsNullOrEmpty(p)))
                {
                    string ver = GetPythonVersion(interpreterPath);

                    if (!string.IsNullOrEmpty(ver))
                    {
                        yield return new PythonInterpreter(interpreterPath, ver);
                    }
                }
            }
        }

        public PythonInterpreter Interpreter { get; }

        public PythonEngine(PythonInterpreter interpreter)
            => this.Interpreter = interpreter;

        public static bool DoesSystemHaveInterpreter => PythonEngine.PythonInterpreters.Any();

        public PythonEngine()
        {
            if(!PythonEngine.DoesSystemHaveInterpreter)
                throw new PythonEngineException("System has no Python interpreter");

            this.Interpreter = PythonEngine.PythonInterpreters.First();
        }

        public ExecutionResult Execute(string pyFile, string[] args)
        {
            string processArgs = $"{pyFile} " + string.Join(" ", args);

            var process = CreateAndExecuteProcess(this.Interpreter.Path, processArgs);

            string stdOut = process.StandardOutput.ReadToEnd();
            string stdErr = process.StandardError.ReadToEnd();
            int exitCode = process.ExitCode;

            return new ExecutionResult(stdOut, stdErr, exitCode);
        }

        public Task<ExecutionResult> ExecuteAsync(string pyFile, string[] args)
        {
            return new Task<ExecutionResult>(() => this.Execute(pyFile, args));
        }
    }
}
