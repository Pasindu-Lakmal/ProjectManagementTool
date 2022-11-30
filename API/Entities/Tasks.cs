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

        public Project Projects { get; set; }

        public int ProjectId { get; set; }

        public AppUser Assignee { get; set; }
    }
}