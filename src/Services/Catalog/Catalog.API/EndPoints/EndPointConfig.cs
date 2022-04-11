namespace Catalog.API.EndPoints;

public class EndPointConfig
{
    public const string APITitle = "Catalog API";
    public const string APIPrefix = $"api";
    public const string VersionPrefix = "v";
    public const int Version1 = 1;
    public static readonly string Version1Str = $"{VersionPrefix}{Version1}";
    public const int Version2 = 2;
    public static readonly string Version2Str = $"{VersionPrefix}{Version2}";
}