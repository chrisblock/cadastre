using System.Collections.Generic;

using Rhino.Etl.Core;
using Rhino.Etl.Core.Operations;

namespace SchemaSurveyor.Etl
{
	public class SimpleEtlProcess : EtlProcess
	{
		private readonly InputCommandOperation _inputOperation;
		private readonly SqlBulkInsertOperation _outputOperation;
		private readonly IEnumerable<IOperation> _transformations;

		public SimpleEtlProcess(InputCommandOperation inputOperation, SqlBulkInsertOperation outputOperation, params IOperation[] transormations)
		{
			_inputOperation = inputOperation;
			_outputOperation = outputOperation;
			_transformations = transormations;
		}

		protected override void Initialize()
		{
			Register(_inputOperation);

			foreach (var transformation in _transformations)
			{
				Register(transformation);
			}

			Register(_outputOperation);
		}
	}
}
