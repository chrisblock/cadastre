using System;
using System.Linq;

using CommandLineArguments;

using SchemaSurveyor.Etl.Surveying;

namespace SchemaSurveyor
{
	public static class Program
	{
		public static int Main(string[] args)
		{
			var exitCode = 1;

			using (var container = StructureMapContainerFactory.Create<SchemaSurveyorRegistry>())
			{
				try
				{
					var arguments = CommandLineArgumentConfigurator.Configure<SchemaSurveyorConfiguration>(args);

					if (String.IsNullOrWhiteSpace(arguments.SurveyName))
					{
						throw new ArgumentException("Survey name must be present and valid for the schema surveyor to function properly.");
					}

					// TODO: do something with the file parameter if standard input is not redirected? maybe?
					/*
					if (String.IsNullOrWhiteSpace(arguments.DatabaseListFile) && (Console.IsInputRedirected == false))
					{
						throw new ArgumentException("There is no database file specified via command line arguments nor is the data being piped in. Unable to continue");
					}
					*/

					var schemaSurveyor = container.GetInstance<ISchemaSurveyor>();

					var result = schemaSurveyor.Survey(arguments.SurveyName);

					var databaseSurveys = result.DatabaseSurveys.ToList();

					foreach (var error in databaseSurveys.Where(x => x.Errors.Any()))
					{
						Console.WriteLine(error.GetConnectionString());

						foreach (var e in error.Errors)
						{
							Console.WriteLine(e.Message);
							Console.WriteLine(e.StackTrace);
						}
					}

#if DEBUG
					if (Console.IsInputRedirected == false)
					{
						Console.WriteLine("Please press any key to continue.");
						Console.ReadKey();
					}
#endif

					if (databaseSurveys.SelectMany(x => x.Errors).Any() == false)
					{
						exitCode = 0;
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					Console.WriteLine(e.StackTrace);
				}
			}

			return exitCode;
		}
	}
}
