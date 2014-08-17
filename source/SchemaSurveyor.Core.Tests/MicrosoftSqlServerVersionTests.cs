using System;
using System.Text;
using System.Threading;

using NUnit.Framework;

// ReSharper disable InconsistentNaming

namespace SchemaSurveyor.Core.Tests
{
	[TestFixture]
	public class MicrosoftSqlServerVersionTests
	{
		private static readonly Lazy<string> LazyVersionString = new Lazy<string>(CreateVersionString, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string VersionString { get { return LazyVersionString.Value; } }

		private static string CreateVersionString()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("Microsoft SQL Server 2012 (SP1) - 11.0.3128.0 (X64)");
			stringBuilder.AppendLine("\tDec 28 2012 20:23:12");
			stringBuilder.AppendLine("\tCopyright (c) Microsoft Corporation");
			stringBuilder.AppendLine("\tDeveloper Edition (64-bit) on Windows NT 6.1 <X64> (Build 7601: Service Pack 1) (Hypervisor)");

			return stringBuilder.ToString();
		}

		[Test]
		public void Parse_Null_ThrowsArgumentNullException()
		{
			Assert.That(() => MicrosoftSqlServerVersion.Parse(null), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void Parse_EmptyString_ThrowsArgumentNullException()
		{
			Assert.That(() => MicrosoftSqlServerVersion.Parse(String.Empty), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void Parse_WhitespaceString_ThrowsArgumentNullException()
		{
			Assert.That(() => MicrosoftSqlServerVersion.Parse(" \r\n\t"), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void Parse_VersionString_CorrectlyParsesYear()
		{
			var version = MicrosoftSqlServerVersion.Parse(VersionString);

			Assert.That(version.Year, Is.EqualTo(2012));
		}

		[Test]
		public void Parse_VersionString_CorrectlyParsesServicePack()
		{
			var version = MicrosoftSqlServerVersion.Parse(VersionString);

			Assert.That(version.ServicePack, Is.EqualTo(1));
		}

		[Test]
		public void Parse_VersionString_CorrectlyParsesEdition()
		{
			var version = MicrosoftSqlServerVersion.Parse(VersionString);

			Assert.That(version.Edition, Is.EqualTo("Developer"));
		}

		[Test]
		public void Parse_VersionString_CorrectlyParsesMajor()
		{
			var version = MicrosoftSqlServerVersion.Parse(VersionString);

			Assert.That(version.Major, Is.EqualTo(11));
		}

		[Test]
		public void Parse_VersionString_CorrectlyParsesMinor()
		{
			var version = MicrosoftSqlServerVersion.Parse(VersionString);

			Assert.That(version.Minor, Is.EqualTo(0));
		}

		[Test]
		public void Parse_VersionString_CorrectlyParsesRevision()
		{
			var version = MicrosoftSqlServerVersion.Parse(VersionString);

			Assert.That(version.Revision, Is.EqualTo(3128));
		}

		[Test]
		public void Parse_VersionString_CorrectlyParsesBuild()
		{
			var version = MicrosoftSqlServerVersion.Parse(VersionString);

			Assert.That(version.Build, Is.EqualTo(0));
		}
	}
}
