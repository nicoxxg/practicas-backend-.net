using System.ComponentModel.DataAnnotations;

namespace universityApiBackend.DTOs
{
    public class UserLoginsDTO
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
