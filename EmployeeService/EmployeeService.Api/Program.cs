using EmployeeService.Api.Configuration;
using EmployeeService.Api.Filters;
using EmployeeService.Application.Extensions;
using EmployeeService.DataAccess.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureSerilog();

builder.Services.AddControllers(options => { options.Filters.Add<CustomExceptionFilter>(); });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddRepositories();
builder.Services.AddUnitOfWork();
builder.Services.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();