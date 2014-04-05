using System;

namespace Cadastre
{
	public abstract class Enums<T>
	{
		public static TEnum Parse<TEnum>(string value, bool ignoreCase = false)
			where TEnum : T
		{
			var result = (TEnum) Enum.Parse(typeof (TEnum), value, ignoreCase);

			return result;
		}
	}

	public class Enums : Enums<Enum>
	{
	}
}
