using Microsoft.EntityFrameworkCore;
using Todo.DataAccess.Data;
using Todo.Models.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// adding controllers
builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); ;

// connecting db string to application
builder.Services.AddDbContext<TodoDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adding AutoMapper to Application
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

// Adding CORS Policy to Application
builder.Services.AddCors(options=> 
{
    options.AddPolicy(name: "Default", policy =>
    {
        policy.WithOrigins("http://localhost:3000/").AllowAnyHeader().AllowAnyMethod();
    });  
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

