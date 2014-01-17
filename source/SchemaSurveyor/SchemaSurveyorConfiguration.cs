using CommandLineArguments;

namespace SchemaSurveyor
{
	public class SchemaSurveyorConfiguration
	{
		[CommandLineArgument("n", "name")]
		public string SurveyName { get; set; }

		[CommandLineArgument("f", "file")]
		public string DatabaseListFile { get; set; }
	}
}
