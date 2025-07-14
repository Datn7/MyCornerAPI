namespace MyCornerAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public string Provider { get; set; }
        public string PasswordHash { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
