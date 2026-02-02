using System.ComponentModel.DataAnnotations;

namespace courseregister.Dtos
{
    public class CourseReadDto
    {
        public int Id { get; set; }
        [Required,MaxLength(50)]
        public string Title { get; set; }
        [Required,MaxLength(50)]
        public int Credits { get; set; }

        public bool IsActive { get; set; }
    }
}
