﻿using System.Web.Mvc;
using System.Web.Routing;

namespace Cadastre
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
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
