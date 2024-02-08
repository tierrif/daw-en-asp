using System.ComponentModel.DataAnnotations;

namespace daw_en_2324.Models
{
    public class CurricularProgram
    {
        public int CurricularProgramId { get; set; }
        [Required]
        public string? Name { get; set; }

        public virtual ICollection<Uc> Ucs { get; set; }
    }
}
