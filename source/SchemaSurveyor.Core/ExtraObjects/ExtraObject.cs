namespace SchemaSurveyor.Core.ExtraObjects
{
	public class ExtraObject
	{
		public int Survey { get; set; }
		public int Database { get; set; }
		public ObjectType Type { get; set; }
		public string Parent { get; set; }
		public string Name { get; set; }
	}
}
