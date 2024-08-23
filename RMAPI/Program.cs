using Microsoft.EntityFrameworkCore;
using RMAPI.Models.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*builder.Services.AddCors(options=>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
    });
}
    );*/
builder.Services.AddCors(o => o.AddPolicy("MyPolicy",builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
})
    );
builder.Services.AddControllers();
builder.Services.AddDbContext<ResourceContext>(x =>
x.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("MyPolicy");
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
