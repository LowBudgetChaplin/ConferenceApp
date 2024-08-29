using ConferenceAPI.Data;
using Microsoft.EntityFrameworkCore;
using Twilio;

var builder = WebApplication.CreateBuilder(args);
var accountSid = Environment.GetEnvironmentVariable("ACCOUNT_SID");
var authToken = Environment.GetEnvironmentVariable("AUTH_TOKEN");

if(accountSid == null && authToken == null)
{
    throw new InvalidOperationException("Twilio Account SID and Auth Token cannot be null");
}

TwilioClient.Init(accountSid, authToken);
// Add services to the container.
//builder.Services.AddSingleton(new TwilioCredentials(accountSid, authToken));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SerbanCorodescuDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();