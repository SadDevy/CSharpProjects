
using System;

namespace ArgumentParsing
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Args arg = new Args("l.p#.d*", args);
				bool logging = arg.GetBoolean('l');
				int port = arg.GetInt('p');
				string directory = arg.GetString('d');
				ExecuteApplication(logging, port, directory);
			}
			catch (ArgsException e)
			{
				Console.WriteLine("Argument error: %s\n", e.Message);
			}
		}


		static void ExecuteApplication(bool logging, int port, string directory)
		{
			Console.WriteLine("Executing application...");
			Console.WriteLine("logging = {0}, port = {1}, directory = {2}",
				logging, port, directory);
		}
	}
}
