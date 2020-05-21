using Microsoft.AspNetCore.Mvc;
using tutorial9.Models;

namespace tutorial9.Services
{
    public interface IDbService
    {
        public IActionResult GetStudents();
        public IActionResult AddStudent(AddStudent student);
        public IActionResult PromoteStudents(PromoteStudent promote);
        public IActionResult DeleteStudent(DeleteStudent delete);
    }
}