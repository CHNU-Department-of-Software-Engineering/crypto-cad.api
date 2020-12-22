namespace CryptoCAD.Domain.Entities.Users
{
    public class User : Entity
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public string PasswordHash { get; set; }
        public string Salt { get; set; }
    }
}