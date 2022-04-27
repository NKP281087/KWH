using KWH.BAL.IRepository;
using KWH.BAL.RepositoryImplementation;
using KWH.DAL.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString("KWHConnection");
builder.Services.AddDbContext<KWHContext>(options =>
{
    options.UseSqlServer(connection);
});

//builder.Services.Configure<ApiBehaviorOptions>(o =>
//{
//    o.InvalidModelStateResponseFactory = actionContext =>
//        new BadRequestObjectResult(actionContext.ModelState);
//});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IAdminBALService, AdminBALService>();
 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
