using DotNetCrudApiLatest.Data;
using DotNetCrudApiLatest.Models;
using DotNetCrudApiLatest.Models.DTO;
using DotNetCrudApiLatest.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotNetCrudApiLatest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;

        IConfiguration _configuration;
        public StudentController(IStudentRepository studentRepository , IConfiguration configuration)
        {
            this.studentRepository = studentRepository;
            _configuration = configuration;
        }

       //   IConfiguration  _configuration;
      // public StudentController(IConfiguration configuration)
       // {
       //     _configuration = configuration;
       // }


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
        [Authorize]
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
        [Route("DeleteEmployeeById/{id:Guid}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var deleteStd = await studentRepository.DeleteById(id);

            if (deleteStd == null)
            {
                return NotFound(); 
            }

            return Ok(deleteStd);

        }


        [HttpGet]
        [Route("GetEmpById/{id:Guid}")]

        public async Task<IActionResult> GetEmpById([FromRoute] Guid id)
        {
            var getById =await studentRepository.GetEmpById(id);

            if(getById == null)
            {
                return NotFound(id);
            }

            return Ok(getById);
        }



        [HttpPut]
        [Route("UpdateStdById/{id:Guid}")]

        public  async Task<IActionResult> UpdateStdById([FromRoute] Guid id, [FromBody] Student student)
        {

            var Updadestd = await studentRepository.UpdateStdById(id , student);
            if (Updadestd == null)
            {
                return NotFound();
            }

            return Ok(Updadestd);


        }




        private string GetToken(GetLogin logindata)
        {
            var claims = new List<Claim>
            {
        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
        new Claim("Name",logindata.Name),
        new Claim("Email",logindata.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var SignIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: SignIn);

            string Tokenkey = new JwtSecurityTokenHandler().WriteToken(Token);

            return Tokenkey;
        }





        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> LoginStd([FromBody] Login login)
        {

            var loginData = await studentRepository.Login(login);

            if(loginData == null)
            {
                return NotFound();
            }

            var getLogin = new GetLogin
            {
                Name = loginData.Name,
                Email = loginData.Email
            };

           var accessToken = GetToken(getLogin);

            var dto = new GetLogin
            {
                Name = loginData.Name,
                Email = loginData.Email,
              AccessToken = accessToken
             
            };
            return Ok(dto);
        }
          


      
        













    }
}
