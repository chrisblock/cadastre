using System;
using System.Linq;

using NUnit.Framework;

using SchemaSurveyor.Core.Surveys.Impl;
using SchemaSurveyor.Etl.Surveying;
using SchemaSurveyor.Etl.Surveying.Impl;

namespace SchemaSurveyor.Etl.Tests
{
	[TestFixture]
	public class SchemaSurveyorTests
	{
		private static readonly string ServerName = Environment.MachineName;
		private const string ReferenceDatabaseName = "ReferenceDatabase";
		private const string OtherDatabaseName = "OtherDatabase";

		private ISchemaSurveyor _surveyor;

		private static void CleanTestDatabases()
		{
			DatabaseHelper.DropDatabase(ServerName, ReferenceDatabaseName);

			DatabaseHelper.DropDatabase(ServerName, OtherDatabaseName);
		}

		private static void BuildTestDatabases()
		{
			DatabaseHelper.CreateDatabase(ServerName, ReferenceDatabaseName);

			DatabaseHelper.CreateDatabase(ServerName, OtherDatabaseName);

			DatabaseHelper.CreateReferenceObjects(ServerName, ReferenceDatabaseName);

			DatabaseHelper.CreateOtherObjects(ServerName, OtherDatabaseName);
		}

		[SetUp]
		public void TestSetUp()
		{
			CleanTestDatabases();

			BuildTestDatabases();

			_surveyor = new Surveying.Impl.SchemaSurveyor(new TestDatabaseListFactory(), new SurveyRepository(), new DatabaseSchemaSurveyor());
		}

		[TearDown]
		public void TestTearDown()
		{
			//CleanTestDatabases();
		}

		[Test]
		public void Test()
		{
			var result = _surveyor.Survey("Test Survey");

			Assert.That(result.DatabaseSurveys.SelectMany(x => x.Errors), Is.Empty);
		}
	}
}
