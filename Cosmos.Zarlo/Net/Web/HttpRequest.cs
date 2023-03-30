using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Cosmos.Zarlo.Net.Web;

public class HttpRequest
{
    public Uri Uri { get; set; }
    public HttpMethod Method { get; set; }
    public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    public string? Body { get; set; }

    public string MakePayload1_1()
    {
        var sb = new StringBuilder();

        sb.AppendLine($@"{Method.AsString()} {Uri.PathAndQuery} HTTP/1.1");
        sb.AppendLine($@"Host: {Uri.Host}:{Uri.Port}");
        foreach (var header in Headers)
        {
            sb.AppendLine($@"{header.Key}: {header.Value}");
        }

        return sb.ToString();
    }
}