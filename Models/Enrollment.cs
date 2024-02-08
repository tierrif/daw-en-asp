namespace daw_en_2324.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        public virtual Student Student { get; set; }
        public virtual Uc Uc { get; set; }
    }
}
