namespace MyCornerAPI.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; } // FK to User
        public string AboutMe { get; set; }
        public string Interests { get; set; } // comma-separated, for now
        public string ReactionsJson { get; set; } // JSON string for reactions
        public string ProfilePictureUrl { get; set; }
        public string CoverPhotoUrl { get; set; }

        public User User { get; set; }
    }
}
