using System.Collections.Generic;

namespace SchemaSurveyor.Core.ExtraObjects
{
	public class ExtraObjectCollection
	{
		public IEnumerable<ExtraObject> Columns { get; set; }
		public IEnumerable<ExtraObject> Functions { get; set; }
		public IEnumerable<ExtraObject> Indexes { get; set; }
		public IEnumerable<ExtraObject> Principals { get; set; }
		public IEnumerable<ExtraObject> Schemas { get; set; }
		public IEnumerable<ExtraObject> Servers { get; set; }
		public IEnumerable<ExtraObject> StoredProcedures { get; set; }
		public IEnumerable<ExtraObject> Synonyms { get; set; }
		public IEnumerable<ExtraObject> Tables { get; set; }
		public IEnumerable<ExtraObject> Triggers { get; set; }
		public IEnumerable<ExtraObject> Views { get; set; }
	}
}
