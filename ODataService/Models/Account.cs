namespace ODataService.Models
{
    public class Account
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime ExpiresIn { get; set; } = DateTime.MinValue;
        public string TokenType { get; set; } = string.Empty;
    }
}
