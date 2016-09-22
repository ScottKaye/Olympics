using ContextEditor;
using Olympics.Models;

namespace Olympics
{
	internal class Program
	{
		private static void Main()
		{
			using (var manager = new ContextManager<OlympicsContext>())
			{
				manager.Start();
			}
		}
	}
}