
using System.Text;

namespace Cosmos.Zarlo.Net.Web;

public class HttpResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    public string? Body { get; set; }
    
}