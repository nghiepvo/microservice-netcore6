namespace Catalog.API.EndPoints;
public static class MessagesConfig
{
    private const string Messages = "Messages";
    public static readonly string Required = $"{Messages}:Required";
    public static readonly string TooShort = $"{Messages}:TooShort";
    public static readonly string Invalid = $"{Messages}:Invalid";
    public static readonly string Fail = $"{Messages}:Fail";
}