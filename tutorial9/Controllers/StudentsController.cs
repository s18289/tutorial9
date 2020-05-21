using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        [HttpPost("promote/student")]
        public IActionResult PromoteStudent(PromoteStudent promote)
        {
            return dbService.PromoteStudents(promote);
        }
    }
}