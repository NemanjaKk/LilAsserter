using LilAsserter;
using LilAsserter.Asserter;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AsserterOptions>(options =>
{
    options.EnableLogging = true;
});
builder.Services.AddScoped<IAsserter>(sp =>
{
    var options = sp.GetRequiredService<IOptions<AsserterOptions>>().Value;
    var logger = sp.GetRequiredService<ILogger<AsserterService>>();
    return new AsserterService(options, logger);
}); builder.Services.AddScoped<AsserterExceptionFilter>();
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
