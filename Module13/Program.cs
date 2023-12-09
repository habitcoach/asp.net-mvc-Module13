var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

// Define an Employee class


app.MapGet("/employee", () =>
{
    // Create a list of Employee objects (you can load this data from a database or other source)
    var employees = new List<Employee>
    {
        new Employee { Name = "John Doe", Email = "john.doe@example.com", Address = "123 Main St" },
        new Employee { Name = "Jane Smith", Email = "jane.smith@example.com", Address = "456 Elm St" }
        // Add more employees as needed
    };

    return employees;
})
.WithName("GetEmployeeDetails");


app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
public class Employee
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}