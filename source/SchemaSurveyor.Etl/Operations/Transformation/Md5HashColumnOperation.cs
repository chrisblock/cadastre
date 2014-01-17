using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

using Rhino.Etl.Core;
using Rhino.Etl.Core.Operations;

namespace SchemaSurveyor.Etl.Operations.Transformation
{
	public class Md5HashColumnOperation : AbstractOperation
	{
		private readonly string _columnName;
		private readonly MD5 _hash;

		public Md5HashColumnOperation(string columnName)
		{
			_columnName = columnName;
			_hash = MD5.Create();
		}

		public override IEnumerable<Row> Execute(IEnumerable<Row> rows)
		{
			foreach (var row in rows)
			{
				if (row.Contains(_columnName))
				{
					var stringData = String.Format("{0}", row[_columnName]);

					var bytes = Encoding.UTF8.GetBytes(stringData);

					var hashBytes = _hash.ComputeHash(bytes);

					var hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

					row[_columnName] = hash;
				}

				yield return row;
			}
		}

		public override void Dispose()
		{
			_hash.Dispose();

			base.Dispose();
		}
	}
}
