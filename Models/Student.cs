using System.ComponentModel.DataAnnotations;

namespace daw_en_2324.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public string? Name { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
