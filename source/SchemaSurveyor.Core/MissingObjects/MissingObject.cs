namespace SchemaSurveyor.Core.MissingObjects
{
	public class MissingObject
	{
		public int Survey { get; set; }
		public int Database { get; set; }
		public ObjectType Type { get; set; }
		public string Parent { get; set; }
		public string Name { get; set; }
	}
}
