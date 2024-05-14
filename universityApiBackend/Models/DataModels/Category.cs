using System.ComponentModel.DataAnnotations;

namespace universityApiBackend.Models.DataModels
{
    public class Category:BaseEntity
    {
        [Required]
        public string name { get; set; } = string.Empty;

        public ICollection<Course> Courses = new List<Course>(); 
    }
}
