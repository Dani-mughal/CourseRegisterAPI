using courseregister.Dtos;
using courseregister.Model;
using Microsoft.AspNetCore.Mvc;

namespace courseregister.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CourseController : ControllerBase
    {
        // In-memory storage (for demo purposes)
        private static readonly List<CourseModel> Courses = new()
        {
            new CourseModel { ID = 1, Title = "Introduction to Programming", Credits = 3, IsActive = true },
            new CourseModel { ID = 2, Title = "Data Science", Credits = 3, IsActive = true },
            new CourseModel { ID = 3, Title = "Business Intelligence", Credits = 3, IsActive = false },
            new CourseModel { ID = 4, Title = "Embedded Systems", Credits = 3, IsActive = true },
            new CourseModel { ID = 5, Title = "Computer Networks", Credits = 3, IsActive = false },
            new CourseModel { ID = 6, Title = "Database", Credits = 3, IsActive = true },
            new CourseModel { ID = 7, Title = "Data Structure And Algorithms", Credits = 3, IsActive = true }
        };

        // ✅ GET all courses
        [HttpGet]
        public IActionResult GetAllCourses()
        {
            var courseDtos = Courses.Select(c => new CourseReadDto
            {
                Id = c.ID,
                Title = c.Title,
                Credits = c.Credits,
                IsActive = c.IsActive
            }).ToList();

            return Ok(courseDtos);
        }

        // ✅ GET course by ID
        [HttpGet("{id:int}")]
        public IActionResult GetCourseById(int id)
        {
            var course = Courses.FirstOrDefault(c => c.ID == id);
            if (course == null)
                return NotFound(new { Message = "Course not found" });

            var dto = new CourseReadDto
            {
                Id = course.ID,
                Title = course.Title,
                Credits = course.Credits,
                IsActive = course.IsActive
            };

            return Ok(dto);
        }

        // ✅ GET active courses
        [HttpGet("active")]
        public IActionResult GetActiveCourses()
        {
            var activeCourses = Courses
                .Where(c => c.IsActive)
                .Select(c => new CourseReadDto
                {
                    Id = c.ID,
                    Title = c.Title,
                    Credits = c.Credits
                })
                .ToList();

            return Ok(activeCourses);
        }

        // ✅ POST create course
        [HttpPost]
        public IActionResult CreateCourse([FromBody] CourseCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newCourse = new CourseModel
            {
                ID = Courses.Max(c => c.ID) + 1,
                Title = dto.Title,
                Credits = dto.Credits,
                IsActive = dto.IsActive
            };

            Courses.Add(newCourse);

            // Return the created resource
            return CreatedAtAction(nameof(GetCourseById), new { id = newCourse.ID }, newCourse);
        }

        // ✅ PUT update course
        [HttpPut("{id:int}")]
        public IActionResult UpdateCourse(int id, [FromBody] CourseUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var course = Courses.FirstOrDefault(c => c.ID == id);
            if (course == null)
                return NotFound(new { Message = "Course not found" });

            course.Title = dto.Title;
            course.Credits = dto.Credits;
            course.IsActive = dto.IsActive;

            return NoContent(); // Standard for successful PUT without returning data
        }

        // ✅ DELETE course
        [HttpDelete("{id:int}")]
        public IActionResult DeleteCourse(int id)
        {
            var course = Courses.FirstOrDefault(c => c.ID == id);
            if (course == null)
                return NotFound(new { Message = "Course not found" });

            Courses.Remove(course);
            return NoContent(); // Standard for successful DELETE
        }
    }
}
