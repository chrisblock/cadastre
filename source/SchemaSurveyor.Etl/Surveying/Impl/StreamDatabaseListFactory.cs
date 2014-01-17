using System;
using System.Collections.Generic;
using System.IO;

using SchemaSurveyor.Core.Surveys;

namespace SchemaSurveyor.Etl.Surveying.Impl
{
	public class StreamDatabaseListFactory : IDatabaseListFactory, IDisposable
	{
		private readonly Stream _stream;

		public StreamDatabaseListFactory(Stream stream)
		{
			_stream = stream;
		}

		public IEnumerable<DatabaseSurvey> BuildDatabaseList()
		{
			var result = new List<DatabaseSurvey>();

			using (var reader = new StreamReader(_stream))
			{
				var hasReferenceSchema = false;

				while (reader.EndOfStream == false)
				{
					var line = reader.ReadLine();

					if (String.IsNullOrWhiteSpace(line) == false)
					{
						var items = line.Split(',');

						if (items.Length == 3)
						{
							var server = items[0].Trim();
							var database = items[1].Trim();
							var isReference = items[2].Trim();

							bool reference;

							var isReferenceSchema = (String.IsNullOrWhiteSpace(isReference) == false) && Boolean.TryParse(isReference, out reference) && reference;

							if (hasReferenceSchema && isReferenceSchema)
							{
								throw new ArgumentException("More than one database cannot be the reference schema.");
							}

							hasReferenceSchema |= isReferenceSchema;

							var item = new DatabaseSurvey
							{
								Server = server,
								Database = database,
								IsReferenceSchema = isReferenceSchema
							};

							result.Add(item);
						}
						else
						{
							throw new ArgumentException(String.Format("'{0}' is invalid.", line));
						}
					}
				}

				if (hasReferenceSchema == false)
				{
					throw new ArgumentException("Could not complete schema survey. No reference schema defined.");
				}
			}

			return result;
		}

		~StreamDatabaseListFactory()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);

			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				// dispose managed resources

				_stream.Dispose();
			}

			// disponse native resources
		}
	}
}
