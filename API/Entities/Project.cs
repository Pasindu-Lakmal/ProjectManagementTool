using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Projects")]
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
    }
}