using System.ComponentModel.DataAnnotations;

namespace universityApiBackend.Models.DataModels
{
    public class User : BaseEntity
    {
        [Required,StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required, StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        [Required,EmailAddress]
        public string Email { get; set; } = String.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
        public Rol rol { get; set; }
    }
}
