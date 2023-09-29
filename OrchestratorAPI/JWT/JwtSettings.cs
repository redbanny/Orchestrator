namespace OrchestratorAPI.JWT.Filters
{
    public class JwtSettings
    {
        public const string ConfigSectionName = "JwtSettings";
        public string AuthScheme { get; set; }
        public string Url { get; set; }
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string SecretKey { get; set; }
        
    }
}