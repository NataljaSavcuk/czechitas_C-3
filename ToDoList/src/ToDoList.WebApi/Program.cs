using ToDoList.Persistence;
using ToDoList.Persistence.Repository;
using ToDoList.Domain.Models;

var builder = WebApplication.CreateBuilder(args);
{
    //Configure DI
    builder.Services.AddControllers();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<ToDoItemsContext>();
    builder.Services.AddScoped<IRepository<ToDoItem>, ToDoItemRepository>();
}

var app = builder.Build();
{
    //Configure Middleware (HTTP request pipeline)
    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI(config => config.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoList API V1"));
}

app.Run();
