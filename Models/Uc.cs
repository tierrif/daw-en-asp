using System.ComponentModel.DataAnnotations;

namespace daw_en_2324.Models
{
    public class Uc
    {
        public int UcId { get; set; }
        [Required]
        public string? Name { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
