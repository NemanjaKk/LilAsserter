using LilAsserter;
using LilAsserter.Asserter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<AsserterService>(serviceProvider =>
{
    var options = new AsserterOptions
    {
        EnableLogging = true
    };
    return new(options, serviceProvider);
});
builder.Services.AddScoped<AsserterExceptionFilter>();
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
