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
					p = $.getJSON(serverListUrl + '/' + value, function (result, status, jqXHR) {
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
							id: i,
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

				var index = databases.indexOf(model),
					newReferenceDatabase = '';

				if ((databases().length > 0) && (model.id === +referenceDatabase())) {
					if (index === (databases().length - 1)) {
						newReferenceDatabase = String(databases()[databases().length - 1].id);
					}
					else {
						newReferenceDatabase = String(databases()[index + 1].id);
					}

					referenceDatabase(newReferenceDatabase);
				}

				databases.remove(model);

				var x = alreadyAdded[model.server];

				delete x[model.database];

				if (availableDatabases().length === 0) {
					availableDatabases.push(model);
				}
				else {
					var start = 0,
						end = availableDatabases().length,
						it,
						count,
						step;
					
					count = end - start;
					
					while (count > 0)
					{
						it = start;
						step = count >> 2;
						it += step;
						if (model.id >= availableDatabases()[it].id)
						{
							start = ++it;
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
						databases: databases().map(function (x, i) {
							return {
								server: x.server,
								database: x.database,
								isReference: i === +referenceDatabase()
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
