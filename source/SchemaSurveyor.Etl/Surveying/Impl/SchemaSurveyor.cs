using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SchemaSurveyor.Core;
using SchemaSurveyor.Core.Surveys;

namespace SchemaSurveyor.Etl.Surveying.Impl
{
	public class SchemaSurveyor : ISchemaSurveyor
	{
		private readonly IDatabaseListFactory _databaseListFactory;
		private readonly ISurveyRepository _surveyRepository;
		private readonly IDatabaseSchemaSurveyor _databaseSchemaSurveyor;

		public SchemaSurveyor(IDatabaseListFactory databaseListFactory, ISurveyRepository surveyRepository, IDatabaseSchemaSurveyor databaseSchemaSurveyor)
		{
			_databaseListFactory = databaseListFactory;
			_surveyRepository = surveyRepository;
			_databaseSchemaSurveyor = databaseSchemaSurveyor;
		}

		public Survey Survey(string surveyName)
		{
			var survey = new Survey
			{
				Name = surveyName
			};

			survey.Id = _surveyRepository.Insert(surveyName);

			var databaseSurveys = new ConcurrentBag<DatabaseSurvey>();

			var connectableDatabases = new List<DatabaseSurvey>();

			var targets = GetTargetDatabases();

			foreach (var database in targets)
			{
				database.SurveyId = survey.Id;

				if (database.HadConnectionError == false)
				{
					connectableDatabases.Add(database);
				}

				// TODO: convert this to bulk insert using Rhino??
				_surveyRepository.InsertDatabaseSurvey(database);
			}

			// TODO: for many databases, this is untenable
			Parallel.ForEach(connectableDatabases, x =>
			{
				var databaseSurvey = _databaseSchemaSurveyor.Survey(x);

				databaseSurveys.Add(databaseSurvey);
			});

			foreach (var databaseSurvey in databaseSurveys)
			{
				_surveyRepository.UpdateDatabaseSurvey(databaseSurvey);
			}

			_surveyRepository.Update(survey.Id);

			survey.DatabaseSurveys = databaseSurveys;

			return survey;
		}

		private IEnumerable<DatabaseSurvey> GetTargetDatabases()
		{
			var targets = _databaseListFactory.BuildDatabaseList()
				.ToList();

			Parallel.ForEach(targets, x => x.HadConnectionError = (ConnectionTester.CanConnect(x.GetConnectionStringBuilder()) == false));

			return targets;
		}
	}
}
