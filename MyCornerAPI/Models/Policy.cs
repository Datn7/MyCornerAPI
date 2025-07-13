namespace MyCornerAPI.Models
{
    public class Policy
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PolicyNumber { get; set; }
        public string Type { get; set; }
        public string CoverageDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

    }
}
