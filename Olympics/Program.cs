using ContextEditor;
using Olympics.Models;

namespace Olympics
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			using (var manager = new ContextManager<OlympicsContext>())
			{
				manager.Start();
			}
		}
	}
}