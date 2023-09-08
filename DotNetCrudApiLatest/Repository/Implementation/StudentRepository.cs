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




    }
}
