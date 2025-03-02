namespace IdentityManager.API.Options;

public class CorsOptions
{
    public string NameCors { get; set; } = null!;
    public string[] AllowedOrigins { get; set; } = null!;
    public string[] AllowedMethods { get; set; } = null!;
    public string[] AllowedHeaders { get; set; } = null!;

    //public string[] ExposedHeaders { get; set; } = null!;
    //public bool AllowCredentials { get; set; }
    //public int PreflightMaxAge { get; set; }
}