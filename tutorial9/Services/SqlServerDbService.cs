using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using tutorial9.Models;

namespace tutorial9.Services
{
    public class SqlServerDbService : ControllerBase, IDbService
    {
        public readonly s18289Context context;
        private Enrollment enrollment = new Enrollment();
        private Student newStudent = new Student();


        public SqlServerDbService(s18289Context _context)
        {
            context = _context;
        }

        public IActionResult GetStudents()
        {
            return Ok(context.Student.ToList());
        }

        public IActionResult AddStudent(AddStudent student)
        {
            int idStudy, idEnrollment;
            idStudy = context.Studies.Where(s => s.Name == student.StudiesName).Select(s => s.IdStudy).FirstOrDefault();
            if (idStudy == 0)
            {
                return BadRequest("Such study name doesn't exists!");
            }

            idEnrollment = context.Enrollment.Where(s => s.Semester == student.Semester && s.IdStudy == idStudy && s.StartDate == DateTime.Today).Select(s => s.IdEnrollment).FirstOrDefault();

            if (idEnrollment != 0)
            {
                enrollment.IdEnrollment = idEnrollment;
            }
            else
            {
                if (context.Enrollment.Any(e => e.IdEnrollment > 0))
                {
                    idEnrollment = context.Enrollment.Max(e => e.IdEnrollment) + 1;
                }
                else
                {
                    idEnrollment = 1;
                }
            }

            enrollment.IdEnrollment = idEnrollment;
            enrollment.Semester = student.Semester;
            enrollment.IdStudy = idStudy;
            enrollment.StartDate = DateTime.Now;

            context.Enrollment.Add(enrollment);
            context.SaveChanges();

            if (context.Student.Any(s => s.IndexNumber == student.IndexNumber))
            {
                return BadRequest("Such index number already exists!");
            }
            else
            {
                newStudent.IndexNumber = student.IndexNumber;
                newStudent.FirstName = student.FirstName;
                newStudent.LastName = student.LastName;
                newStudent.BirthDate = student.BirthDate;
                newStudent.IdEnrollment = enrollment.IdEnrollment;
            }

            context.Student.Add(newStudent);
            context.SaveChanges();

            return Ok("Student was added successfully");
        }

        public IActionResult PromoteStudents(PromoteStudent promote)
        {
            if(!context.Studies.Any(s => s.Name == promote.StudiesName))
            {
                return BadRequest("Such study name doesn't exists");
            }

            int idStudy = context.Studies.Where(s => s.Name == promote.StudiesName).Select(s => s.IdStudy).FirstOrDefault();

            if(context.Enrollment.Any(e => e.Semester == promote.Semester && e.IdStudy == idStudy))
            {
                context.Enrollment.Where(e => e.Semester == promote.Semester && e.IdStudy == idStudy).ToList().ForEach(e => e.Semester = e.Semester + 1); 
            }

            context.SaveChanges();
        
            return Ok("Student was promoted successfully");
        }

        public IActionResult DeleteStudent(DeleteStudent delete)
        {
            if(context.Student.Any(s => s.IndexNumber == delete.IndexNumber))
            {
                context.Student.Remove(context.Student.Where(s => s.IndexNumber == delete.IndexNumber).FirstOrDefault());
            }
            else
            {
                return BadRequest("Student with such index number doesn't exists");
            }

            context.SaveChanges();

            return Ok("Student was deleted successfully");
        }
    }
}