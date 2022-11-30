using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Tasks")]
    public class Tasks
    {
        public int TasksId { get; set; }
        public int Description { get; set; }
        public DateTime DueDate { get; set; }
        public string status { get; set; }

        public Work Works { get; set; }

        public int WorkId { get; set; }

        public AppUser Assignee { get; set; }
    }
}