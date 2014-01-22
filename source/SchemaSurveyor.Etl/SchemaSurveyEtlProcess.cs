using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

using Rhino.Etl.Core;

using SchemaSurveyor.Etl.Operations.BulkInsert;
using SchemaSurveyor.Etl.Operations.Input;
using SchemaSurveyor.Etl.Operations.Transformation;

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
			_etlProcesses.Add(new SimpleEtlProcess(new CheckConstraintInputOperation(source), new CheckConstraintBulkInsertOperation(destination), new AddDatabaseSurveyIdColumnOperation(databaseSurveyId)));
			_etlProcesses.Add(new SimpleEtlProcess(new ColumnInputOperation(source), new ColumnBulkInsertOperation(destination), new AddDatabaseSurveyIdColumnOperation(databaseSurveyId)));
			_etlProcesses.Add(new SimpleEtlProcess(new DatabasePermissionInputOperation(source), new DatabasePermissionBulkInsertOperation(destination), new AddDatabaseSurveyIdColumnOperation(databaseSurveyId)));
			_etlProcesses.Add(new SimpleEtlProcess(new DatabasePrincipalInputOperation(source), new DatabasePrincipalBulkInsertOperation(destination), new AddDatabaseSurveyIdColumnOperation(databaseSurveyId)));
			_etlProcesses.Add(new SimpleEtlProcess(new DatabaseRoleMemberInputOperation(source), new DatabaseRoleMemberBulkInsertOperation(destination), new AddDatabaseSurveyIdColumnOperation(databaseSurveyId)));
			_etlProcesses.Add(new SimpleEtlProcess(new DefaultConstraintInputOperation(source), new DefaultConstraintBulkInsertOperation(destination), new AddDatabaseSurveyIdColumnOperation(databaseSurveyId)));
			_etlProcesses.Add(new SimpleEtlProcess(new ForeignKeyColumnInputOperation(source), new ForeignKeyColumnBulkInsertOperation(destination), new AddDatabaseSurveyIdColumnOperation(databaseSurveyId)));
			_etlProcesses.Add(new SimpleEtlProcess(new ForeignKeyInputOperation(source), new ForeignKeyBulkInsertOperation(destination), new AddDatabaseSurveyIdColumnOperation(databaseSurveyId)));
			_etlProcesses.Add(new SimpleEtlProcess(new FunctionInputOperation(source), new FunctionBulkInsertOperation(destination), new AddDatabaseSurveyIdColumnOperation(databaseSurveyId)));
			_etlProcesses.Add(new SimpleEtlProcess(new IndexColumnInputOperation(source), new IndexColumnBulkInsertOperation(destination), new AddDatabaseSurveyIdColumnOperation(databaseSurveyId)));
			_etlProcesses.Add(new SimpleEtlProcess(new IndexInputOperation(source), new IndexBulkInsertOperation(destination), new AddDatabaseSurveyIdColumnOperation(databaseSurveyId)));
			_etlProcesses.Add(new SimpleEtlProcess(new KeyConstraintInputOperation(source), new KeyConstraintBulkInsertOperation(destination), new AddDatabaseSurveyIdColumnOperation(databaseSurveyId)));
			_etlProcesses.Add(new SimpleEtlProcess(new LinkedLoginInputOperation(source), new LinkedLoginBulkInsertOperation(destination), new AddDatabaseSurveyIdColumnOperation(databaseSurveyId)));
			_etlProcesses.Add(new SimpleEtlProcess(new ParameterInputOperation(source), new ParameterBulkInsertOperation(destination), new AddDatabaseSurveyIdColumnOperation(databaseSurveyId)));
			_etlProcesses.Add(new SimpleEtlProcess(new ProcedureInputOperation(source), new ProcedureBulkInsertOperation(destination), new AddDatabaseSurveyIdColumnOperation(databaseSurveyId)));
			_etlProcesses.Add(new SimpleEtlProcess(new RemoteLoginInputOperation(source), new RemoteLoginBulkInsertOperation(destination), new AddDatabaseSurveyIdColumnOperation(databaseSurveyId)));
			_etlProcesses.Add(new SimpleEtlProcess(new SchemaInputOperation(source), new SchemaBulkInsertOperation(destination), new AddDatabaseSurveyIdColumnOperation(databaseSurveyId)));
			_etlProcesses.Add(new SimpleEtlProcess(new SqlLoginInputOperation(source), new SqlLoginBulkInsertOperation(destination), new AddDatabaseSurveyIdColumnOperation(databaseSurveyId)));
			_etlProcesses.Add(new SimpleEtlProcess(new SynonymInputOperation(source), new SynonymBulkInsertOperation(destination), new AddDatabaseSurveyIdColumnOperation(databaseSurveyId)));
			_etlProcesses.Add(new SimpleEtlProcess(new TableInputOperation(source), new TableBulkInsertOperation(destination), new AddDatabaseSurveyIdColumnOperation(databaseSurveyId)));
			_etlProcesses.Add(new SimpleEtlProcess(new TriggerInputOperation(source), new TriggerBulkInsertOperation(destination), new AddDatabaseSurveyIdColumnOperation(databaseSurveyId)));
			_etlProcesses.Add(new SimpleEtlProcess(new ViewInputOperation(source), new ViewBulkInsertOperation(destination), new AddDatabaseSurveyIdColumnOperation(databaseSurveyId)));
			// _etlProcesses.Add(new SimpleEtlProcess(new SqlModuleInputOperation(source), new SqlModuleBulkInsertOperation(destination), new AddDatabaseSurveyIdColumnOperation(databaseSurveyId)));
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
