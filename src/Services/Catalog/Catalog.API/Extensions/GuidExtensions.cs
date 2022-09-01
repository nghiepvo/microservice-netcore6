using MongoDB.Bson;

namespace Common.Libraries.API.Extensions;

public static class GuidExtensions
{
    /// <summary>
    /// Only Use to convert a Guid that was once an ObjectId
    /// </summary>
    public static ObjectId AsObjectId(this Guid guid)
    {
        var bytes = guid.ToByteArray().Take(12).ToArray();
        var oid = new ObjectId(bytes);
        return oid;
    }

    /// <summary>
    /// Only Use to convert a Guid that was once an ObjectId string
    /// </summary>
    public static string AsStringObjectId(this Guid guid) => AsObjectId(guid).ToString();
}