using System;

namespace Cosmos.Zarlo.Net.Web;

public enum HttpMethod
{
    Get,
    Head,
    Post,
    Put,
    Delete,
    Options,
    Trace,
    Patch
}

public static class MethodEx
{
    public static string AsString(this HttpMethod me)
    {
        return me switch
        {
            HttpMethod.Get => "GET",
            HttpMethod.Head => "HEAD",
            HttpMethod.Post => "POST",
            HttpMethod.Put => "PUT",
            HttpMethod.Delete => "DELETE",
            HttpMethod.Options => "OPTIONS",
            HttpMethod.Trace => "TRACE",
            HttpMethod.Patch => "PATCH",
            _ => throw new ArgumentOutOfRangeException(nameof(me), me, null)
        };
    }
}