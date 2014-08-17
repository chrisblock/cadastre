using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SchemaSurveyor.Core
{
	public class MicrosoftSqlServerVersion
	{
		public static MicrosoftSqlServerVersion Parse(string version)
		{
			if (String.IsNullOrWhiteSpace(version))
			{
				throw new ArgumentNullException("version");
			}

			var lines = version.Split('\n')
				.Select(x => x.Trim())
				.ToArray();

			int year;
			int servicePack;
			int major;
			int minor;
			int revision;
			int build;

			ParseFirstLine(lines[0]
				, out year
				, out servicePack
				, out major
				, out minor
				, out revision
				, out build
				);

			string edition;

			ParseFourthLine(lines[3], out edition);

			// TODO: parse strings like this:
			/*
			Microsoft SQL Server 2012 (SP1) - 11.0.3128.0 (X64) 
				Dec 28 2012 20:23:12 
				Copyright (c) Microsoft Corporation
				Developer Edition (64-bit) on Windows NT 6.1 <X64> (Build 7601: Service Pack 1) (Hypervisor)
			*/

			var result = new MicrosoftSqlServerVersion
			{
				Year = year,
				ServicePack = servicePack,
				Edition = edition,
				Major = major,
				Minor = minor,
				Revision = revision,
				Build = build
			};

			return result;
		}

		private static void ParseFirstLine(string line, out int year, out int servicePack, out int major, out int minor, out int revision, out int build)
		{
			var regex = new Regex(@"^Microsoft SQL Server (\d{4}) \(SP(\d+)\) - (\d+\.\d+\.\d+\.\d+) \(([^\)]+)\)$");

			var match = regex.Match(line);

			year = -1;
			servicePack = -1;
			major = -1;
			minor = -1;
			revision = -1;
			build = -1;

			if (match.Groups.Count == 5)
			{
				year = Int32.Parse(match.Groups[1].Value);
				servicePack = Int32.Parse(match.Groups[2].Value);

				var specificVersion = match.Groups[3].Value
					.Split('.')
					.Select(Int32.Parse)
					.ToArray();

				major = specificVersion[0];
				minor = specificVersion[1];
				revision = specificVersion[2];
				build = specificVersion[3];
			}
		}

		private static void ParseFourthLine(string line, out string edition)
		{
			var regex = new Regex(@"^(\w+) Edition \(\d+-bit\) on .+$");

			var match = regex.Match(line);

			edition = null;

			if (match.Groups.Count == 2)
			{
				edition = match.Groups[1].Value;
			}
		}

		public string Edition { get; set; }
		public int Year { get; set; }
		public bool IsVirtualized { get; set; }
		public int ServicePack { get; set; }
		public int Major { get; set; }
		public int Minor { get; set; }
		public int Revision { get; set; }
		public int Build { get; set; }
	}
}
