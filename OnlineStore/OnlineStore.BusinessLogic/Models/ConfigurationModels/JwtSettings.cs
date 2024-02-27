namespace OnlineStore.BusinessLogic.Models.ConfigurationModels
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecurityKey { get; set; }
        public int ExpireInMinutes { get; set; }
    }
}
