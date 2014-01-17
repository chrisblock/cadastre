using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NUnit.Framework;

using SchemaSurveyor.Core.Surveys;
using SchemaSurveyor.Etl.Surveying.Impl;

// ReSharper disable InconsistentNaming

namespace SchemaSurveyor.Etl.Tests
{
	public class GenericObjectEqualityComparer<T> : IEqualityComparer<T>
	{
		private readonly Func<T, T, bool> _equals;
		private readonly Func<T, int> _hashCode;

		public GenericObjectEqualityComparer(Func<T, T, bool> equals, Func<T, int> hashCode)
		{
			_equals = equals;
			_hashCode = hashCode;
		}

		public bool Equals(T x, T y)
		{
			return _equals(x, y);
		}

		public int GetHashCode(T obj)
		{
			return _hashCode(obj);
		}
	}

	[TestFixture]
	public class StreamDatabaseListFactoryTests
	{
		private static bool DatabaseSurveyEquals(DatabaseSurvey left, DatabaseSurvey right)
		{
			bool result;

			if (ReferenceEquals(null, left) && ReferenceEquals(null, right))
			{
				result = true;
			}
			else if (ReferenceEquals(null, left) || ReferenceEquals(null, right))
			{
				result = false;
			}
			else if (ReferenceEquals(left, right))
			{
				result = true;
			}
			else
			{
				result = String.Equals(left.Server, right.Server) && String.Equals(left.Database, right.Database) && left.IsReferenceSchema.Equals(right.IsReferenceSchema);
			}

			return result;
		}

		private static int GetDatabaseSurveyHashCode(DatabaseSurvey survey)
		{
			var str = String.Format("Server:{0};Database:{1};IsReferenceSchema:{2};", survey.Server, survey.Database, survey.IsReferenceSchema);

			return str.GetHashCode();
		}

		[Test]
		public void BuildDataBaseList_EmptyStream_ThrowsException()
		{
			using (var stream = new MemoryStream(Encoding.ASCII.GetBytes(String.Empty), false))
			{
				using (var databaseListFactory = new StreamDatabaseListFactory(stream))
				{
					Assert.That(() => databaseListFactory.BuildDatabaseList(), Throws.ArgumentException);
				}
			}
		}

		[Test]
		public void BuildDataBaseList_StreamContainingMultipleEmptyLines_ThrowsException()
		{
			var builder = new StringBuilder();

			builder.AppendLine(String.Empty);
			builder.AppendLine(String.Empty);
			builder.AppendLine(String.Empty);
			builder.AppendLine(String.Empty);
			builder.AppendLine(String.Empty);
			builder.AppendLine(String.Empty);
			builder.AppendLine(String.Empty);

			using (var stream = new MemoryStream(Encoding.ASCII.GetBytes(builder.ToString()), false))
			{
				using (var databaseListFactory = new StreamDatabaseListFactory(stream))
				{
					Assert.That(() => databaseListFactory.BuildDatabaseList(), Throws.ArgumentException);
				}
			}
		}

		[Test]
		public void BuildDatabaseList_StreamContainingInvalidLines_ThrowsException()
		{
			var builder = new StringBuilder();

			builder.AppendLine("Server1, Database1, true");
			builder.AppendLine("Server1, Database2, false");
			builder.AppendLine("Server1, Database3, false");
			builder.AppendLine("Server1, Database4, false");
			builder.AppendLine("Server1, Database5, false");
			builder.AppendLine("Server1, Database6, false");
			builder.AppendLine("Server1, Database7, false");
			builder.AppendLine("this line is invalid");

			using (var stream = new MemoryStream(Encoding.ASCII.GetBytes(builder.ToString()), false))
			{
				using (var databaseListFactory = new StreamDatabaseListFactory(stream))
				{
					Assert.That(() => databaseListFactory.BuildDatabaseList(), Throws.ArgumentException);
				}
			}
		}

		[Test]
		public void BuildDatabaseList_StreamContainingNoReferenceSchema_ThrowsException()
		{
			var builder = new StringBuilder();

			builder.AppendLine("Server1, Database1, false");
			builder.AppendLine("Server1, Database2, false");
			builder.AppendLine("Server1, Database3, false");
			builder.AppendLine("Server1, Database4, false");
			builder.AppendLine("Server1, Database5, false");
			builder.AppendLine("Server1, Database6, false");
			builder.AppendLine("Server1, Database7, false");

			using (var stream = new MemoryStream(Encoding.ASCII.GetBytes(builder.ToString()), false))
			{
				using (var databaseListFactory = new StreamDatabaseListFactory(stream))
				{
					Assert.That(() => databaseListFactory.BuildDatabaseList(), Throws.ArgumentException);
				}
			}
		}

		[Test]
		public void BuildDatabaseList_StreamContainingOnlyValidLines_OutputsCorrectDatabaseList()
		{
			var builder = new StringBuilder();

			builder.AppendLine("Server1, Database1, true");
			builder.AppendLine("Server1, Database2, false");
			builder.AppendLine("Server1, Database3, false");
			builder.AppendLine("Server1, Database4, false");
			builder.AppendLine("Server1, Database5, false");
			builder.AppendLine("Server1, Database6, false");
			builder.AppendLine("Server1, Database7, false");

			using (var stream = new MemoryStream(Encoding.ASCII.GetBytes(builder.ToString()), false))
			{
				using (var databaseListFactory = new StreamDatabaseListFactory(stream))
				{
					var expected = new[]
					{
						new DatabaseSurvey
						{
							Server = "Server1",
							Database = "Database1",
							IsReferenceSchema = true
						},
						new DatabaseSurvey
						{
							Server = "Server1",
							Database = "Database2",
							IsReferenceSchema = false
						},
						new DatabaseSurvey
						{
							Server = "Server1",
							Database = "Database3",
							IsReferenceSchema = false
						},
						new DatabaseSurvey
						{
							Server = "Server1",
							Database = "Database4",
							IsReferenceSchema = false
						},
						new DatabaseSurvey
						{
							Server = "Server1",
							Database = "Database5",
							IsReferenceSchema = false
						},
						new DatabaseSurvey
						{
							Server = "Server1",
							Database = "Database6",
							IsReferenceSchema = false
						},
						new DatabaseSurvey
						{
							Server = "Server1",
							Database = "Database7",
							IsReferenceSchema = false
						}
					};

					var databaseList = databaseListFactory.BuildDatabaseList().ToList();

					Assert.That(databaseList, Has.Count.EqualTo(7));
					Assert.That(databaseList, Is.EquivalentTo(expected).Using(new GenericObjectEqualityComparer<DatabaseSurvey>(DatabaseSurveyEquals, GetDatabaseSurveyHashCode)));
				}
			}
		}
	}
}
