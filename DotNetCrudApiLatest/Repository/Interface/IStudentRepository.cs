using DotNetCrudApiLatest.Models;

namespace DotNetCrudApiLatest.Repository.Interface
{
    public interface IStudentRepository
    {
        Task<Student> CreateAsync(Student student);


        Task<List<Student>> GetAllAsync();



        Task<Student > DeleteById(Guid id);
      
    }
}
