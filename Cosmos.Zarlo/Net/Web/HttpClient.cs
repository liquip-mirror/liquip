using System.Text;

namespace Cosmos.Zarlo.Net.Web;

public class HttpClient
{

    private readonly Uri? _baseUri;
    public string UserAgent { get; set; } = "zarlo_cosmos/0.5.0";
    
    
    public HttpClient()
    {
        _baseUri = null;
    }
    public HttpClient(Uri baseUri)
    {
        _baseUri = baseUri;
    }

    public HttpResponse Get(Uri uri) => MakeRequest(new HttpRequest()
    {
        Uri = uri,
        Method = HttpMethod.Get
    });

    public HttpResponse MakeRequest(HttpRequest request)
    {
        if (_baseUri != null) request.Uri = new Uri(_baseUri, request.Uri);
        
        request.Headers.Add("user-agent", UserAgent);
        
        using var client = new TcpClient(0);
        
        client.Connect(request.Uri);
        client.Send( Encoding.ASCII.GetBytes(request.MakePayload1_1()));
        var content = client.Receive();

        global::System.Console.WriteLine(Encoding.ASCII.GetString(content));

        return new HttpResponse()
        {

        };
    }


}