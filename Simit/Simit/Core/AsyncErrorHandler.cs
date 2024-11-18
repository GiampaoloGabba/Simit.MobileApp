using System;
using System.Diagnostics;

namespace Simit.Core
{
	public static class AsyncErrorHandler
	{
		public static void HandleException(Exception exception)
		{
			Debug.WriteLine("Async Error Handler: " + exception.Message);
			Debug.WriteLine(exception.Source);
			Debug.WriteLine(exception.StackTrace);

#if DEBUG
			//Debugger.Break();
#endif
		}
	}
}