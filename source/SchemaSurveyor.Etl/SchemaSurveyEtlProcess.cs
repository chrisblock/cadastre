using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

using Rhino.Etl.Core;

using SchemaSurveyor.Etl.Operations.Transformation;
using SchemaSurveyor.Etl.Tables;

namespace SchemaSurveyor.Etl
{
	public class SchemaSurveyEtlProcess : IDisposable
	{
		private readonly ICollection<EtlProcess> _etlProcesses = new List<EtlProcess>();

		public SchemaSurveyEtlProcess(int databaseSurveyId, string source, string destination) : this(databaseSurveyId, new SqlConnectionStringBuilder(source), new SqlConnectionStringBuilder(destination))
		{
		}

		public SchemaSurveyEtlProcess(int databaseSurveyId, SqlConnectionStringBuilder source, SqlConnectionStringBuilder destination)
		{
			var tableDefinitionType = typeof (ITableDefinition);

			var genericEtlProcessType = typeof (SimpleEtlProcess<>);

			var addDatabaseSurveyIdColumnOperation = new AddDatabaseSurveyIdColumnOperation(databaseSurveyId);

			var etlProcesses = AppDomain.CurrentDomain.GetAssemblies()
				.Where(x => x.GlobalAssemblyCache == false)
				.Where(x => x.IsDynamic == false)
				.SelectMany(x => x.GetExportedTypes())
				.Where(x => x.IsInterface == false)
				.Where(x => x.IsAbstract == false)
				.Where(tableDefinitionType.IsAssignableFrom)
				.Select(x => genericEtlProcessType.MakeGenericType(x))
				.Select(x => Activator.CreateInstance(x, source, destination, addDatabaseSurveyIdColumnOperation))
				.Cast<EtlProcess>();

			foreach (var process in etlProcesses)
			{
				_etlProcesses.Add(process);
			}
		}

		public void Execute()
		{
			Parallel.ForEach(_etlProcesses, x => x.Execute());
		}

		public IEnumerable<Exception> GetAllErrors()
		{
			var result = _etlProcesses.SelectMany(x => x.GetAllErrors());

			return result;
		}

		~SchemaSurveyEtlProcess()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);

			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				// dispose managed resources here

				foreach (var etlProcess in _etlProcesses)
				{
					etlProcess.Dispose();
				}

				_etlProcesses.Clear();
			}

			// dispose unmanaged resources here
		}
	}
}
