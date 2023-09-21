namespace Olympus.SimpleDataApi;

public class SimpleDataApiConfigurationHandler
{
    private static readonly SimpleDataApiConfigurationHandler instance = new();

    public static SimpleDataApiConfigurationHandler Instance()
    {
        return instance;
    }

    public SimpleDataApiConfigurationHandler SetActorIdParameterName(string name)
    {
        ParameterConfiguration.ActorId = name;
        return instance;
    }

    public SimpleDataApiConfigurationHandler SetActorUsernameParameterName(string name)
    {
        ParameterConfiguration.ActorUsername = name;
        return instance;
    }
}
