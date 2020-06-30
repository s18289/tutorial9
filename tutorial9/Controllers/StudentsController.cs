using Microsoft.AspNetCore.Mvc;
using tutorial9.Models;
using tutorial9.Services;

namespace tutorial9.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        public readonly IDbService dbService;

        public StudentsController(IDbService _dbService)
        {
            dbService = _dbService;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            return dbService.GetStudents();
        }

        [HttpPost("add/student")]
        public IActionResult AddStudent(AddStudent student)
        {
            return dbService.AddStudent(student);
        }

        [HttpPut("promote/student")]
        public IActionResult PromoteStudent(PromoteStudent promote)
        {
            return dbService.PromoteStudents(promote);
        }

        [HttpDelete("delete/student")]
        public IActionResult DeleteStudent(DeleteStudent delete)
        {
            return dbService.DeleteStudent(delete);
        }
    }
}