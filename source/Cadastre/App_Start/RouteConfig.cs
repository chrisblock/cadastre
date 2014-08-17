using System.Web.Mvc;
using System.Web.Routing;

namespace Cadastre
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.MapRoute(
				name: "CreateSurvey",
				url: "Surveys/Create",
				defaults: new { controller = "Surveys", action = "Create" }
			);

			routes.MapRoute(
				name: "Survey",
				url: "Surveys/{surveyId}",
				defaults: new { controller = "Surveys", action = "Survey" }
			);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Surveys", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
