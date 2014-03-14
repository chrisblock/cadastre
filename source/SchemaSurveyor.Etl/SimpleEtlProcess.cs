using System.Collections.Generic;
using System.Data.SqlClient;

using Rhino.Etl.Core;
using Rhino.Etl.Core.Operations;

using SchemaSurveyor.Etl.Operations.BulkInsert;
using SchemaSurveyor.Etl.Operations.Input;
using SchemaSurveyor.Etl.Tables;

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

	public class SimpleEtlProcess<T> : EtlProcess
		where T : ITableDefinition, new()
	{
		private readonly InputCommandOperation _inputOperation;
		private readonly SqlBulkInsertOperation _outputOperation;
		private readonly IEnumerable<IOperation> _transformations;

		public SimpleEtlProcess(SqlConnectionStringBuilder source, SqlConnectionStringBuilder destination, params IOperation[] transormations)
		{
			_inputOperation = new SimpleInputCommandOperation<T>(source);
			_outputOperation = new SimpleBulkInsertOperation<T>(destination);
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
