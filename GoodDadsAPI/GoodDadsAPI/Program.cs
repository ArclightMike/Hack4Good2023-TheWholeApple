using GoodDadsAPI.Services.Common;

var builder = WebApplication.CreateBuilder(args);

//ToDo: Get this off the configuration. Something like: builder.Configuration.GetValue<string>("GoodDadsDatabase");
GoodDadsResources.DatabaseConnectionString = "";

builder.Services.AddApplication();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

