using CRUD_WEB_API.DTO;
using CRUD_WEB_API.Helper;
using CRUD_WEB_API.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly IStudent _IRepository;

        public StudentController(IStudent IRepository)
        {
            _IRepository = IRepository;
        }

        [HttpPost]
        [Route("CreateStudent")]
        public async Task<MessageHelper> CreateStudent(StudentDTO Create)
        {
            var msg = await _IRepository.CreateStudent(Create);
            return msg;

        }

        [HttpPut]
        [Route("EditStudent")]
        public async Task<MessageHelper> EditStudent(StudentDTO edit)
        {
            var msg = await _IRepository.EditStudent(edit);
            return msg;

        }

        [HttpGet]
        [Route("GetStudentById")]
        public async Task<IActionResult> GetStudentById(long studentId)
        {

            var dt = await _IRepository.GetStudentById(studentId);
            if (dt == null)
            {
                return NotFound();
            }
            return Ok(dt);

        }

        [HttpGet]
        [Route("GetStudentList")]
        public async Task<IActionResult> GetStudentList()
        {

            var dt = await _IRepository.GetStudentList();
            if (dt == null)
            {
                return NotFound();
            }
            return Ok(dt);

        }

        

    }
}

