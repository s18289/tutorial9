using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tutorial9.Models;

namespace tutorial9.Services
{
    public interface IDbService
    {
        public IActionResult GetStudents();
        public IActionResult AddStudent(AddStudent student);
        public IActionResult PromoteStudents(PromoteStudent promote);
    }
}