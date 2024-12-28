namespace StreamOverlay.Data;

public class Config
{
    public DiscordProviderConfiguration Discord { get; set; } = new DiscordProviderConfiguration();
    public YoutubeProviderConfiguration Youtube { get; set; } = new YoutubeProviderConfiguration();
    public AutomatedMessageConfiguration AutomatedMessage { get; set; } = new AutomatedMessageConfiguration();
    
    public class DiscordProviderConfiguration
    {
        public string Token { get; set; } = "";
        public long[] ChannelIds { get; set; } = [];
        public bool IncludeBotMessages { get; set; } = false;
        public bool RequiredRole { get; set; } = false;
        public long[] RequiredRoleId { get; set; } = [];
    }

    public class YoutubeProviderConfiguration
    {
        public string ApiKey { get; set; } = "";
    }

    public class AutomatedMessageConfiguration
    {
        public int IntervalSeconds { get; set; } = 300;
        public bool Randomize { get; set; } = false;
        public string SenderName { get; set; } = "";
        public string SenderAvatarUrl { get; set; } = "";
        public string[] Messages { get; set; } = [];
    }
}