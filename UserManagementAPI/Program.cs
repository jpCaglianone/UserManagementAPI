using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Data;
using UserManagementAPI.Data.Repository;
using UserManagementAPI.Midlewares;
using UserManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddAuthorization();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("TemporaryDb");
});

var app = builder.Build();

app.UseMiddleware<MErrorHandling>();
app.UseMiddleware<MReqRespLog>();
app.UseMiddleware<MTokenValidation>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    //  app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserManagementAPI v1"));
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseMiddleware<MTokenValidation>();

app.UseAuthorization();

app.MapControllers();

app.Run();
