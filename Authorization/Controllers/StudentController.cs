using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authorization.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class StudentController : ControllerBase
    {

        [HttpGet]
        [Authorize]
        public List<Student> GetStudents()
        {
            using (var _context = DbContextFactory.CreateCisMainDbContext())
            {
                List<Student> studentlist = _context.Student.ToList();
                return studentlist;
            }
        }


        [HttpPost]
        public ActionResult AddStudent([FromBody]Student stud)
        {
            try
            {
                using (var _context = DbContextFactory.CreateCisMainDbContext())
                {
                    _context.Student.Add(stud);
                    _context.SaveChanges();
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return Ok("Added Successfully");

        }
    }
    }
