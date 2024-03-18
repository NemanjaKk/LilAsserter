using LilAsserter;
using LilAsserter.Asserter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<AsserterService>(sp =>
{
    var options = new AsserterOptions
    {
        EnableLogging = true
    };
    var logger = sp.GetRequiredService<ILogger<AsserterService>>();
    return new(options, logger);
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
