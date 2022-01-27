namespace digify.Models;

public class ServerInformation
{
    public string ApiVersion { get; }
    public DateTime CurrentDate { get; }

    public ServerInformation(string apiVersion)
    {
        this.ApiVersion = apiVersion;
        this.CurrentDate = new DateTime();
    }
}