namespace MyGameIO.Api.Configuration
{
    public class SecurityConfig
    {
        public string Secret { get; set; }
        public int Expiration { get; set; }
        public string Emitter { get; set; }
        public string Validation { get; set; }

    }
}
