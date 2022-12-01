namespace API.DTOs
{
    public class WorkDto
    {
        public int WorkId { get; set; }
        public string WorkName { get; set; }
        public string WorkDescription { get; set; }
        public int AppUserId { get; set; }
    }
}