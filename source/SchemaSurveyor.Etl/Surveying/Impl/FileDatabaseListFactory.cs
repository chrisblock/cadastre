using System.IO;

namespace SchemaSurveyor.Etl.Surveying.Impl
{
	public class FileDatabaseListFactory : StreamDatabaseListFactory
	{
		public FileDatabaseListFactory(string fileName) : base(File.OpenRead(fileName))
		{
		}
	}
}
