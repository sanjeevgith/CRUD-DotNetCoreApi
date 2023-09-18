using DotNetCrudApiLatest.Models;

namespace DotNetCrudApiLatest.Repository.Interface
{
    public interface IStudentRepository
    {
        Task<Student> CreateAsync(Student student);


        Task<List<Student>> GetAllAsync();



        Task<Student > DeleteById(Guid id);


        Task<Student> GetEmpById(Guid id);


        Task<Student> UpdateStdById(Guid id, Student student);


        Task<Student> Login(Login login);


    }
}
