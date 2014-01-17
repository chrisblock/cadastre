using System.Text;
using System.Web.Mvc;

namespace Cadastre
{
	public abstract class Controller : System.Web.Mvc.Controller
	{
		protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
		{
			return new JsonDotNetResult
			{
				Data = data,
				ContentType = contentType,
				ContentEncoding = contentEncoding,
				JsonRequestBehavior = behavior
			};
		}
	}
}
