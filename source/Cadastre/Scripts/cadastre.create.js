(function (cadastre, $, ko, undefined) {
	(function (create) {
		var CreateSurveyView = function (serverListUrl, surveyRequestUrl) {
			var self = this,
				alreadyAdded = {},
				serverDatabases = {},
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
				var p;

				availableDatabases.removeAll();
				selectedDatabases.removeAll();

				if (!serverDatabases[value]) {
					p = $.getJSON(serverListUrl + '/' + value + '/Databases', function (result, status, jqXHR) {
						if (result && $.isArray(result)) {
							serverDatabases[value] = result;
						}
					});
				}
				else {
					p = $.when(serverDatabases[value]);
				}

				p.done(function (x) {
					var item;

					for (var i = 0, len = x.length; i < len; i++) {
						item = {
							id: String(i),
							server: value,
							database: x[i]
						};
						
						if (!alreadyAdded[value] || !alreadyAdded[value][item.database]) {
							availableDatabases.push(item);
						}
					}
				});
			});

			self.addSelectedDatabase = function (model, e) {
				e.preventDefault();
				e.stopPropagation();

				if (!referenceDatabase()) {
					referenceDatabase(String(selectedDatabases()[0].id));
				}

				var s = model.server();

				if (alreadyAdded[s] === undefined) {
					alreadyAdded[s] = {};
				}

				$.each(selectedDatabases(), function (index, d) {
					if (alreadyAdded[s][d.database] === undefined) {
						alreadyAdded[s][d.database] = true;

						model.databases.push(d);

						availableDatabases.remove(d);
					}
				});

				return false;
			};

			self.removeSelectedDatabase = function (model, e) {
				e.preventDefault();
				e.stopPropagation();

				var dbs = databases(),
					index = databases.indexOf(model),
					newReferenceDatabase = referenceDatabase(),
					available = availableDatabases();

				if ((dbs.length > 1) && (model.id === referenceDatabase())) {
					if (index === (dbs.length - 1)) {
						newReferenceDatabase = String(dbs[index - 1].id);
					}
					else {
						newReferenceDatabase = String(dbs[index + 1].id);
					}
				}
				else if (dbs.length === 1) {
					newReferenceDatabase = '';
				}

				referenceDatabase(newReferenceDatabase);

				databases.remove(model);

				var x = alreadyAdded[model.server];

				delete x[model.database];

				if (available.length === 0) {
					availableDatabases.push(model);
				}
				else {
					var start = 0,
						count = available.length,
						it,
						step;

					while (count > 0)
					{
						step = count >> 2;
						it = start + step;
						
						if (model.id >= available[it].id)
						{
							start = it + 1;
							count -= step + 1;
						}
						else {
							count = step;
						}
					}

					availableDatabases.splice(start, 0, model);
				}

				return false;
			};

			self.create = function (model, e) {
				e.preventDefault();
				e.stopPropagation();

				var data = {
						name: name(),
						databases: databases().map(function (x) {
							return {
								server: x.server,
								database: x.database,
								isReference: x.id === referenceDatabase()
							};
						})
					};

				$.post(surveyRequestUrl, data).done(function (result, status, jqXHR) {
					// TODO: show a message or redirect to list of surveys
					debugger;
				});

				return false;
			};
		};

		create.init = function (serverListUrl, surveyRequestUrl, createSurveyId) {
			var model = new CreateSurveyView(serverListUrl, surveyRequestUrl),
				createSurveyElement = $(createSurveyId).get(0);

			ko.applyBindings(model, createSurveyElement);
			
			$.get(serverListUrl).done(function (result, status, jqXHR) {
				if (result && $.isArray(result)) {
					model.availableServers(result);
				}
			});
		};
	} (cadastre.create = cadastre.create || {}));
} (this.cadastre = this.cadastre || {}, jQuery, ko));
