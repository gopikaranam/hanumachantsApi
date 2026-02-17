var builder = WebApplication.CreateBuilder(args);

//DI
builder.Services.AddSingleton<TableService>();

builder.Services.AddCors(o => o.AddPolicy("AllowAll",
    b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAll");

// Enable Swagger
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();
app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapControllers();
app.Run();
