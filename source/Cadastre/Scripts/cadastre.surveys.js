(function (cadastre, $, ko, undefined) {
	(function (surveys) {
		var SurveyListViewModel = function (surveyUrl) {
				var self = this,
					surveys = ko.observableArray();

				self.surveys = surveys;

				self.showSurvey = function (survey, event) {
					var url = surveyUrl.replace('0', survey.id);

					document.location.href = url;
				};
			};

		surveys.init = function (surveysUrl, surveyUrl, surveyListId) {
			var model = new SurveyListViewModel(surveyUrl);

			ko.applyBindings(model, $(surveyListId).get(0));

			$.getJSON(surveysUrl).done(function (result, status, jqXHR) {
				if (result && $.isArray(result)) {
					model.surveys(result);
				}
			});
		};
	} (cadastre.surveys = cadastre.surveys || {}));
} (this.cadastre = this.cadastre || {}, jQuery, ko))
