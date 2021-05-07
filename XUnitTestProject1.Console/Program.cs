using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Threading;
using Xunit.Runners;

namespace XUnitTestProject1.Console
{
	class Program
	{
		static readonly object consoleLock = new object();

		// Use an event to know when we're done
		static readonly ManualResetEvent finished = new ManualResetEvent(false);

		// Start out assuming success; we'll set this to 1 if we get a failed test
		public static int _result = 0;

		static void Main(string[] args)
		{
			string pathOfCurrentlyExecutingAssembly = Assembly.GetExecutingAssembly().Location;
			string folderOfCurrentlyExecutingAssembly = Path.GetDirectoryName(pathOfCurrentlyExecutingAssembly);
			var nameOfAssemblyToTest = $"{typeof(UnitTest1).Assembly.GetName().Name}.dll";
			var testAssemblyToRun = $"{folderOfCurrentlyExecutingAssembly}/{nameOfAssemblyToTest}";

			using AssemblyRunner runner = AssemblyRunner.WithoutAppDomain(testAssemblyToRun);
			runner.OnDiscoveryComplete = OnDiscoveryComplete;
			runner.OnExecutionComplete = OnExecutionComplete;
			runner.OnTestFailed = OnTestFailed;
			runner.OnTestSkipped = OnTestSkipped;

			System.Console.WriteLine("Discovering...");
			runner.Start();

			finished.WaitOne();
			finished.Dispose();
		}

		static void OnDiscoveryComplete(DiscoveryCompleteInfo info)
		{
			lock (consoleLock)
			{
				System.Console.WriteLine($"Running {info.TestCasesToRun} of {info.TestCasesDiscovered} tests...");
			}
		}

		static void OnExecutionComplete(ExecutionCompleteInfo info)
		{
			lock (consoleLock)
			{
				System.Console.WriteLine($"Finished: {info.TotalTests} tests in {Math.Round(info.ExecutionTime, 3)}s ({info.TestsFailed} failed, {info.TestsSkipped} skipped)");
			}

			finished.Set();
		}

		static void OnTestFailed(TestFailedInfo info)
		{
			lock (consoleLock)
			{
				System.Console.ForegroundColor = ConsoleColor.Red;

				System.Console.WriteLine("[FAIL] {0}: {1}", info.TestDisplayName, info.ExceptionMessage);
				if (info.ExceptionStackTrace != null)
				{
					System.Console.WriteLine(info.ExceptionStackTrace);
				}

				System.Console.ResetColor();
			}

			_result = 1;
		}

		static void OnTestSkipped(TestSkippedInfo info)
		{
			lock (consoleLock)
			{
				System.Console.ForegroundColor = ConsoleColor.Yellow;
				System.Console.WriteLine("[SKIP] {0}: {1}", info.TestDisplayName, info.SkipReason);
				System.Console.ResetColor();
			}
		}
	}
}
