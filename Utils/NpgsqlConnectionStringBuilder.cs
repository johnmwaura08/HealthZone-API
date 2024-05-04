internal class NpgsqlConnectionStringBuilder
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Database { get; set; } = string.Empty;
    public object? SslMode { get; set; } 
    public bool TrustServerCertificate { get; set; }
}