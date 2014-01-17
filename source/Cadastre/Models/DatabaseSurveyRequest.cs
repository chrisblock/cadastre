namespace Cadastre.Models
{
	public class DatabaseSurveyRequest
	{
		public string Server { get; set; }

		public string Database { get; set; }

		public bool IsReference { get; set; }
	}
}
