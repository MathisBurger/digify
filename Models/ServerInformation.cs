using System;

namespace digify.Models;

/// <summary>
/// The server information property
/// </summary>
public class ServerInformation
{
    /// <summary>
    /// The version of the API
    /// </summary>
    public string ApiVersion { get; }
    
    /// <summary>
    /// The current date
    /// </summary>
    public DateTime CurrentDate { get; }

    public ServerInformation(string apiVersion)
    {
        ApiVersion = apiVersion;
        CurrentDate = DateTime.Now;
    }
}