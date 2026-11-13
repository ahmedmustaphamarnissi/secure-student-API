namespace StudentApi.DataSimulation;

public static class ConnectionString
{
    public static string Connection { get; set; } = string.Empty;
}

// Backward compatibility alias for StudentDataSimulation
public static class connectionString
{
    public static string connection
    {
        get => ConnectionString.Connection;
        set => ConnectionString.Connection = value;
    }
}