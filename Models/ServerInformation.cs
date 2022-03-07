using System;

namespace digify.Models;

public class ServerInformation
{
    public string ApiVersion { get; }
    public DateTime CurrentDate { get; }

    public ServerInformation(string apiVersion)
    {
        ApiVersion = apiVersion;
        CurrentDate = DateTime.Now;
    }
}