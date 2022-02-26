<Query Kind="Program" />

// All queries automatically reference the 'My Extensions' query (bottom of 'My Queries' or Ctrl+Shift+Y).
// 'My Extensions' is a great place to write general-purpose extension methods and utility classes.

// For instance, suppose you want to define an "ArrayOfOne" method that wraps an object in an array.
// While the following works, it only works for the current query:

void Main()
{
	"Hello world!".Dump();
	123456789012.Dump();
	"".Dump();
	"Hello world!".DumpMono();
	123456789012.DumpMono();

	Console.WriteLine("{0, 10} {1, 1}", '_', '_');
	Console.WriteLine("{0, 8} {1, 7}", '[', ']');
	Console.WriteLine("{0, 7} {1, 6}", '<', '>');
	Console.WriteLine("{0, 11} {1, 2}", '|', '|');
	Console.WriteLine("{0, 11} {1, 1}", 'J', 'L');
	
	decimal zero = 0m;
	decimal fullX = 1234.5678m;
	decimal fullY = 8765.4321m;
	decimal hundred = 100m;
	decimal small = .001m;
	decimal oneTenth = .1m;
	string.Format("{0, 6: 0.0}", zero).DumpMono();
	string.Format("{0}", fullX).DumpMono();
	string.Format("{0}", fullY).DumpMono();
	string.Format("{0, 6:F1}", hundred).DumpMono();
	string.Format("{0, 8}", small).DumpMono();
	string.Format("{0, 6}", oneTenth).DumpMono();

}

public static class MyExtensions
{
	public static T[] ArrayOfOne<T> (this T item) => new T[] { item };


	public static T DumpMono<T>(this T toDump, string heading = null, int? depth = null)
	{
 		Util.WithStyle(toDump, "font-family:consolas").Dump(heading, depth);
 		return toDump;
	}
 }

// Now try cutting the method definition from the class above, and pasting it into the 'My Extensions' query.
// That extension method will now work for all queries.

// TIP: If you have a paid LINQPad edition, pressing F12 over a method call will jump to its definition.
//      This jumps into 'My Extensions', too.
//
// TIP: You can nagivate directly to 'My Extensions' with Ctrl+Shift+Y (see File menu)