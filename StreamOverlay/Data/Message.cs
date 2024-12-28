using Newtonsoft.Json;

namespace StreamOverlay.Data;

public class Message(string provider, string author, string content, string? avatarUrl, string[]? tag, long? timestamp)
{
    public readonly string Provider = provider;
    public readonly string Author = author;
    public readonly string Content = content;
    public readonly string? AvatarUrl = avatarUrl;
    public readonly string[]? Tag = tag;
    public readonly long? Timestamp = timestamp;

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }
    
}