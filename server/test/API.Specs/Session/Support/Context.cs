using System.Net.Http;

namespace API.Specs.Session.Support
{
	public class Context
	{
		public string UserId { get; set; }
		public HttpResponseMessage HttpResponse { get; set; }
	}
}
