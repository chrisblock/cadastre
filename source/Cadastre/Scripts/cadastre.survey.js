(function (cadastre, $, ko, undefined) {
	(function (survey) {
		var titles = {
				'columns': 'Columns',
				'functions': 'Functions',
				'indexes': 'Indexes',
				'principals': 'Principals',
				'schemas': 'Schemas',
				'servers': 'Servers',
				'storedProcedures': 'Stored Procedures',
				'synonyms': 'Synonyms',
				'tables': 'Tables',
				'triggers': 'Triggers',
				'views': 'Views'
			},
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
					isDisabled = ko.computed(function () {
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
				self.isDisabled = isDisabled;
				self.needsParentColumn = needsParentColumn;
			},
			DatabaseSurvey = function (db, urls) {
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
								var title = tab.title(),
									url = urls[title],
									actualUrl = url.replace(/\/0\//i, '/' + id() + '/');

								$.getJSON(actualUrl, function (result, s, x) {
									$.each(result, function (type, objects) {
										var n = titles[type],
											t = new DatabaseSurveyObjectType(n);

										tab.types.push(t);

										t.objects(objects);
									});
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

		survey.init = function (urls, surveyDatabasesUrl, canvasId) {
			var model = new DatabaseSurveyListViewModel();

			ko.applyBindings(model, $(canvasId).get(0));

			$.getJSON(surveyDatabasesUrl).done(function (result, status, jqXHR) {
				if (result && $.isArray(result)) {
					$.each(result, function (i, item) {
						var db = new DatabaseSurvey(item, urls);

						model.databases.push(db);
					});
				}
			});
		};
	} (cadastre.survey = cadastre.survey || {}));
} (this.cadastre = this.cadastre || {}, jQuery, ko))
