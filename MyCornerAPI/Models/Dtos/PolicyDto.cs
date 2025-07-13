namespace MyCornerAPI.Models.Dtos
{
    public class PolicyDto
    {
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; }
        public string PolicyType { get; set; }
        public string Coverage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }
}
