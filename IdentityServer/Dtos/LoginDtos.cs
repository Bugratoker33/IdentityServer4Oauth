namespace IdentityServer.Dtos
{
    public class LoginDtos
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
