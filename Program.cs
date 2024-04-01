using PizzaApi.Models;
using PizzaApi.Utils;
using Microsoft.EntityFrameworkCore;
using System.Configuration;


/*var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";*/
var builder = WebApplication.CreateBuilder(args);



/*builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:5091/api",
                                              "http://localhost:3000");
                      });
});*/

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddDbContext<PizzaApi.Models.Pizza_DBContext>(option => option.UseNpgsql(builder.Configuration.GetConnectionString("con")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
            });

//builder.Services.AddCors();

/*builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigin", builder => builder.AllowAnyOrigin());
});*/

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseCors(MyAllowSpecificOrigins);
//app.UseCors(options => options.AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.UseCors(options =>
    options.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
    );

app.Run();
