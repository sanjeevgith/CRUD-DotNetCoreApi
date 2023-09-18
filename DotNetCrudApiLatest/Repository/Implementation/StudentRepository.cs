using DotNetCrudApiLatest.Data;
using DotNetCrudApiLatest.Models;
using DotNetCrudApiLatest.Models.DTO;
using DotNetCrudApiLatest.Repository.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetCrudApiLatest.Repository.Implementation
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext dbContext;

        public StudentRepository(StudentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Student> CreateAsync(Student student)
        {
             await dbContext.Students.AddAsync(student);
             await dbContext.SaveChangesAsync();
             return student;
        }






        public async Task<List<Student>> GetAllAsync()
        {
            return await dbContext.Students.ToListAsync();
        }

        public async Task<Student> DeleteById(Guid id)
        {
            var studentToDelete = await dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (studentToDelete != null)
            {
                dbContext.Students.Remove(studentToDelete);
                await dbContext.SaveChangesAsync();
            }

            return studentToDelete;
        }

        public async Task<Student> GetEmpById(Guid id)
        {
            var getStudent = await dbContext.Students.FirstOrDefaultAsync(x => x.Id==id);

           if (getStudent != null)
            {
                await dbContext.SaveChangesAsync();
                return getStudent;
            }  
            return getStudent;
        }


        public async Task<Student> UpdateStdById(Guid id, Student student)
        {
            var existingStudent = await  dbContext.Students.FirstOrDefaultAsync(y => y.Id == id);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Phone = student.Phone;
                existingStudent.Address = student.Address;
                existingStudent.Email = student.Email;
                existingStudent.Pincode = student.Pincode;

                await dbContext.SaveChangesAsync();

                return existingStudent;
            }
            return null;

        }



        public async Task<Student> Login(Login login)
        {
            var loginData = await dbContext.Students.FirstOrDefaultAsync(student => student.Name == login.Name && student.Email == login.Email);
            return loginData;
        }




    }
}
