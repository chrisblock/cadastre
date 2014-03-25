using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cadastre
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.MapHttpRoute(
				name: "SurveyApi",
				routeTemplate: "api/Surveys/{surveyId}",
				defaults: new { controller = "Surveys", surveyId = RouteParameter.Optional });

			routes.MapHttpRoute(
				name: "DatabaseSurveyApi",
				routeTemplate: "api/Surveys/{surveyId}/Databases/{databaseSurveyId}",
				defaults: new { controller = "DatabaseSurveys", databaseSurveyId = RouteParameter.Optional });

			routes.MapHttpRoute(
				name: "MissingObjectApi",
				routeTemplate: "api/Surveys/{surveyId}/Databases/{databaseSurveyId}/MissingObjects/{objectType}",
				defaults: new { controller = "MissingObjects", objectType = RouteParameter.Optional });

			routes.MapHttpRoute(
				name: "ExtraObjectApi",
				routeTemplate: "api/Surveys/{surveyId}/Databases/{databaseSurveyId}/ExtraObjects/{objectType}",
				defaults: new { controller = "ExtraObjects", objectType = RouteParameter.Optional });

			routes.MapHttpRoute(
				name: "ServerApi",
				routeTemplate: "api/Servers/{serverName}",
				defaults: new { controller = "Servers", serverName = RouteParameter.Optional });

			routes.MapRoute(
				name: "Databases",
				url: "Servers/{serverName}",
				defaults: new { controller = "Servers", action = "Databases" }
			);

			routes.MapRoute(
				name: "CreateSurvey",
				url: "Surveys/Create",
				defaults: new { controller = "Surveys", action = "Create" }
			);

			routes.MapRoute(
				name: "Surveys",
				url: "Surveys/",
				defaults: new { controller = "Surveys", action = "Surveys" }
			);

			routes.MapRoute(
				name: "Survey",
				url: "Surveys/{surveyId}",
				defaults: new { controller = "Surveys", action = "Survey" }
			);

			routes.MapRoute(
				name: "SurveyDatabases",
				url: "Surveys/{surveyId}/Databases",
				defaults: new { controller = "Surveys", action = "Databases" }
			);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Surveys", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
