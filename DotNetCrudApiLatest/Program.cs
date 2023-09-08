using DotNetCrudApiLatest.Data;
using DotNetCrudApiLatest.Repository.Implementation;
using DotNetCrudApiLatest.Repository.Interface;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//db context
builder.Services.AddDbContext<StudentDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("students")));
//end

//repository service
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
//end repo service


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
