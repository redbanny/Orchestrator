namespace OrchestratorAPI.JWT.Filters
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string SecretKey { get; set; }
        
    }
}