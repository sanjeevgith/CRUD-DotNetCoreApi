using DotNetCrudApiLatest.Data;
using DotNetCrudApiLatest.Models;
using DotNetCrudApiLatest.Models.DTO;
using DotNetCrudApiLatest.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetCrudApiLatest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        [HttpPost]
        [Route("CreateStudent")]
        public async Task<IActionResult> CreateEmployee(CreateStudentDTO request)
        {
          //DTO to Domain model
            var student = new Student
            {
                Name = request.Name,
                Phone = request.Phone,
                Email = request.Email,
                Address = request.Address,
                Pincode = request.Pincode,
            };

             await studentRepository.CreateAsync(student);

            //Domain Model To DTO
            var resoponse = new GetStudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Phone = student.Phone,
                Email = student.Email,
                Address = student.Address,
                Pincode = student.Pincode,
            };
            return Ok(resoponse);
        }




        [HttpGet]
        [Route("GetAllStudents")]
        public async Task<IActionResult> GetAll()
        {
            var students = await studentRepository.GetAllAsync();
            var studentDTOs = new List<GetStudentDTO>();

            foreach (var student in students)
            {
                var dto = new GetStudentDTO
                {
                    Id = student.Id,
                    Name = student.Name,
                    Phone = student.Phone,
                    Email = student.Email,
                    Address = student.Address,
                    Pincode = student.Pincode
                };
                studentDTOs.Add(dto);
            }

            return Ok(studentDTOs);
        }




        [HttpDelete]
        [Route("DeleteEmployeeById{id:Guid}")]
        public IActionResult DeleteById([FromRoute] Guid id)
        {
            var deleteStd = studentRepository.DeleteById(id);

            if (deleteStd == null)
            {
                return NotFound(); 
            }

            return Ok(deleteStd);

        }


    }
}
