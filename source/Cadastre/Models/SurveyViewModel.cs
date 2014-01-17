using System;

namespace Cadastre.Models
{
	public class SurveyViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string User { get; set; }
		public string Machine { get; set; }
		public DateTime Start { get; set; }
		public DateTime? End { get; set; }
	}
}
