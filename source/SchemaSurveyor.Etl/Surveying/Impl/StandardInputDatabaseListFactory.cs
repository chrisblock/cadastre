using System;

namespace SchemaSurveyor.Etl.Surveying.Impl
{
	public class StandardInputDatabaseListFactory : StreamDatabaseListFactory
	{
		public StandardInputDatabaseListFactory() : base(Console.OpenStandardInput())
		{
			if (Console.IsInputRedirected == false)
			{
				throw new ApplicationException("Cannot read database list from standard input if the list of databases is not piped into the application");
			}
		}
	}
}
