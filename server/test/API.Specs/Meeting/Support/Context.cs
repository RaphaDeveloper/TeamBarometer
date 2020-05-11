using System.Net.Http;

namespace API.Specs.Meeting.Support
{
	public class Context
	{
		public string UserId { get; set; }
		public HttpResponseMessage HttpResponse { get; set; }
	}
}
