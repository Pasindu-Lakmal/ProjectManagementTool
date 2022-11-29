using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required] public String Username { get; set; }
        [Required] public String Gender { get; set; }
        
        [Required] public String KnownAs{ get; set; }
        [Required] public DateOnly? DateOfBirth { get; set; } //optional to make required work
        [Required] public String City { get; set; }
        [Required] public String Country { get; set; }

        [Required] [StringLength(8, MinimumLength = 4)]
        public String Password { get; set; }
        
        
    }
}