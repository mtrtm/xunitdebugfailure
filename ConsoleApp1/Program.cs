using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("Looping indefinitely...");

			while (true)
			{
				await Task.Delay(1000);
				Console.WriteLine("in console, and this line is totally debuggable when running in docker-compose");
			}
		}
	}
}
