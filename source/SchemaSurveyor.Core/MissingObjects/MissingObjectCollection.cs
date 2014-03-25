using System.Collections.Generic;

namespace SchemaSurveyor.Core.MissingObjects
{
	public class MissingObjectCollection
	{
		public IEnumerable<MissingObject> Columns { get; set; }
		public IEnumerable<MissingObject> Functions { get; set; }
		public IEnumerable<MissingObject> Indexes { get; set; }
		public IEnumerable<MissingObject> Principals { get; set; }
		public IEnumerable<MissingObject> Schemas { get; set; }
		public IEnumerable<MissingObject> Servers { get; set; }
		public IEnumerable<MissingObject> StoredProcedures { get; set; }
		public IEnumerable<MissingObject> Synonyms { get; set; }
		public IEnumerable<MissingObject> Tables { get; set; }
		public IEnumerable<MissingObject> Triggers { get; set; }
		public IEnumerable<MissingObject> Views { get; set; }
	}
}
