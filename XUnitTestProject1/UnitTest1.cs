using System;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1
{
	public class UnitTest1
	{
		[Fact]
		public async Task Test1()
		{
			for (int i = 0; i < 1000; i++)
			{
				await Task.Delay(1000);
				Console.WriteLine("this is a similar loop, but if you put a breakpoint on this unit test it will never be hit");
			}
		}
	}
}
