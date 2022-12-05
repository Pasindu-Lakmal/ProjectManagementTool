using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Todos")]
    public class Todo
    {
        public int TodoId { get; set; }
        public string  Title { get; set; }
        public string  Description { get; set; }
        public DateTime DueDate { get; set; }
        
        public string status { get; set; }
        public string CreaterName { get; set; } 
        public string AssigneeName { get; set; } 
        public Work Works { get; set; }

        public int WorkId { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
    }
}