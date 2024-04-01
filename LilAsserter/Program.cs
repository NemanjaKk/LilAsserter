using LilAsserter.AsserterNemagus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AsserterOptions>(builder.Configuration.GetSection("AsserterOptions"));
builder.Services.AddAsserter();

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
