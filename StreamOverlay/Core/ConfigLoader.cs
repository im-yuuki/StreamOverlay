using Newtonsoft.Json;

namespace StreamOverlay.Data;

public class ConfigLoader(ILogger<ConfigLoader> logger)
{
    private const string ConfigPath = "config.json";
    public Config Configuration { get; private set; } = new Config();

    public bool Load()
    {
        try {
            using var jsonReader = new JsonTextReader(File.OpenText(ConfigPath));
            Configuration = new JsonSerializer().Deserialize<Config>(jsonReader) ?? throw new NullReferenceException();
            return true;
        }
        catch (Exception e) {
            logger.LogError(e, "Failed to load configuration file");
            return false;
        }
    }
    
    public bool Save()
    {
        try {
            new JsonSerializer().Serialize(new JsonTextWriter(File.CreateText(ConfigPath)), Configuration);
            return true;
        }
        catch (Exception e) {
            logger.LogError(e, "Failed to save configuration file");
            return false;
        }
    }
}