(function (cadastre, $, ko, undefined) {
	(function (survey) {
		var urls = {},
			DatabaseSurveyListViewModel = function () {
				var self = this,
					databases = ko.observableArray();

				self.databases = databases;
			},
			DatabaseSurveyTab = function (t) {
				var self = this,
					title = ko.observable(t),
					id = ko.computed(function () {
						return title().toLowerCase().replace(/\s+/g, '-');
					}),
					types = ko.observableArray();

				self.title = title;
				self.id = id;
				self.types = types;
			},
			DatabaseSurveyObjectType = function (t) {
				var self = this,
					title = ko.observable(t),
					id = ko.computed(function () {
						return title().toLowerCase().replace(/\s+/g, '-');
					}),
					objects = ko.observableArray(),
					isActive = ko.computed(function () {
						var active = (objects().length === 0);

						return active;
					}),
					needsParentColumn = ko.computed(function () {
						var o = objects();

						return ((o.length > 0) && (!!o[0].parent));
					});

				self.title = title;
				self.id = id;
				self.objects = objects;
				self.isActive = isActive;
				self.needsParentColumn = needsParentColumn;
			},
			DatabaseSurvey = function (db) {
				var self = this,
					id = ko.observable(db.id),
					surveyId = ko.observable(db.surveyId),
					server = ko.observable(db.server),
					database = ko.observable(db.database),
					isReferenceSchema = ko.observable(db.isReferenceSchema),
					hadConnectionError = ko.observable(db.hadConnectionError),
					hadEtlError = ko.observable(db.hadEtlError),
					duration = ko.observable(db.duration),
					areDetailsVisible = ko.observable(false),
					areDetailsLoaded = false,
					tabs = ko.observableArray([
						new DatabaseSurveyTab('Missing'),
						new DatabaseSurveyTab('Extra')
					]),
					loadDetails = function () {
						if (areDetailsLoaded === false) {
							areDetailsLoaded = true;

							$.each(tabs(), function (i, tab) {
								var title = tab.title();

								$.each(urls[title], function (type, url) {
									var t = new DatabaseSurveyObjectType(type);

									tab.types.push(t);

									setTimeout(function () {
										var actualUrl = url.replace(/databaseSurveyId/i, id());

										$.getJSON(actualUrl).done(function (r, s, x) {
											if (r && $.isArray(r)) {
												$.each(r, function (j, item) {
													t.objects.push(item);
												});
											}
										});
									}, 0);
								});
							});
						}
					};

				self.id = id;
				self.surveyId = surveyId;
				self.server = server;
				self.database = database;
				self.isReferenceSchema = isReferenceSchema;
				self.hadConnectionError = hadConnectionError;
				self.hadEtlError = hadEtlError;
				self.duration = duration;
				self.areDetailsVisible = areDetailsVisible;
				self.tabs = tabs;

				self.toggleDetails = function () {
					var detailsVisible = areDetailsVisible();

					loadDetails();

					areDetailsVisible((isReferenceSchema() === false) && (detailsVisible === false));
				};
			};

		survey.init = function (urlMap, surveyDatabasesUrl, canvasId) {
			var model = new DatabaseSurveyListViewModel();

			urls = urlMap;

			ko.applyBindings(model, $(canvasId).get(0));

			$.getJSON(surveyDatabasesUrl).done(function (result, status, jqXHR) {
				if (result && $.isArray(result)) {
					$.each(result, function (i, item) {
						var db = new DatabaseSurvey(item);

						model.databases.push(db);
					});
				}
			});
		};
	} (cadastre.survey = cadastre.survey || {}));
} (this.cadastre = this.cadastre || {}, jQuery, ko))
