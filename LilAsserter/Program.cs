using AsserterNemagus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAsserter(new AsserterOptions()
{
    EnableLogging = true
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<AsserterExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
