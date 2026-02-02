using System.ComponentModel.DataAnnotations;

namespace courseregister.Dtos
{
    public class CourseUpdateDto
    {
        [Required,MaxLength(50)]
        public string Title { get; set; }
        [Required,MaxLength(50)]
        public int Credits { get; set; }
        public bool IsActive { get; set; }
    }
}
