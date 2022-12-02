using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Works")]
    public class Work
    {
        public int WorkId { get; set; }
        public string WorkName { get; set; }
        public string WorkDescription { get; set; }
        public string CreaterName { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
        public ICollection<Todo> Todos { get; set; }
    }
}