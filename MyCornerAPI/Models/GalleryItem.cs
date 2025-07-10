namespace MyCornerAPI.Models
{
    public class GalleryItem
    {
        public int Id { get; set; }
        public int UserId { get; set; } // FK to User
        public string ImageUrl { get; set; }
        public string Caption { get; set; }

        public User User { get; set; }
    }
}
