namespace API.DTOs
{
    public class TodoDto
    {
        public int TodoId { get; set; }
        public string  Title { get; set; }
        public string  Description { get; set; }
        public DateTime DueDate { get; set; } 
        public string status { get; set; }
        public string CreaterName { get; set; } 
        public string AssigneeName { get; set; } 
        public string WorkName { get; set; } 
        public int WorkId { get; set; }
        public int AppUserId { get; set; }
    }
}