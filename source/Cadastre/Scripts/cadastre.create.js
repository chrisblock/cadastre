(function (cadastre, $, ko, undefined) {
	(function (create) {
		var CreateSurveyView = function (serverListUrl) {
			var self = this,
				alreadyAdded = {},
				name = ko.observable(),
				server = ko.observable(),
				databases = ko.observableArray(),
				availableServers = ko.observableArray(),
				availableDatabases = ko.observableArray(),
				selectedDatabases = ko.observableArray(),
				referenceDatabase = ko.observable();

			self.name = name;
			self.server = server;
			self.databases = databases;
			self.availableServers = availableServers;
			self.availableDatabases = availableDatabases;
			self.selectedDatabases = selectedDatabases;
			self.referenceDatabase = referenceDatabase;

			server.subscribe(function (value) {
				$.get(serverListUrl + '/' + value, function (data, status, jqXHR) {
					var databaseName;

					availableDatabases.removeAll();
					selectedDatabases.removeAll();

					for (var i = 0, len = data.length; i < len; i++) {
						databaseName = data[i].name;
						if (!alreadyAdded[value] || !alreadyAdded[value][databaseName]) {
							availableDatabases.push(databaseName);
						}
					}
				});
			});

			self.addSelectedDatabases = function (model, e) {
				e.preventDefault();
				e.stopPropagation();

				if (!referenceDatabase()) {
					referenceDatabase('0');
				}

				var s = model.server();

				if (alreadyAdded[s] === undefined) {
					alreadyAdded[s] = {};
				}

				$.each(selectedDatabases(), function (index, d) {
					var selectedDatabase = {
						server: s,
						database: d
					};

					if (alreadyAdded[s][d] === undefined) {
						alreadyAdded[s][d] = true;

						model.databases.push(selectedDatabase);

						availableDatabases.remove(d);
					}
				});
			};
		};

		create.init = function (serverListUrl, surveyRequestUrl, createSurveyId, submitButtonId) {
			var model = new CreateSurveyView(serverListUrl),
				createSurveyElement = $(createSurveyId).get(0);

			ko.applyBindings(model, createSurveyElement);
			
			$.get(serverListUrl).done(function (result, status, jqXHR) {
				if (result && $.isArray(result)) {
					var servers = [];

					for (var i = 0, len = result.length; i < len; i++) {
						servers.push(result[i].name);
					}

					model.availableServers(servers);
				}
			});
			
			$(submitButtonId).click(function (e) {
				e.preventDefault();
				e.stopPropagation();

				var json = ko.toJS(ko.dataFor(createSurveyElement)),
					referenceDatabase = json.referenceDatabase,
					data = {
						name: json.name,
						databases: json.databases.map(function (x, i) {
							return {
								server: x.server,
								database: x.database,
								isReference: i === +referenceDatabase
							};
						})
					};

				$.post(surveyRequestUrl, data).done(function (result, status, jqXHR) {
					debugger;
				});

				return false;
			});
		};
	} (cadastre.create = cadastre.create || {}));
} (this.cadastre = this.cadastre || {}, jQuery, ko));
