using ConferenceAPI.Data;
using Microsoft.EntityFrameworkCore;
using Twilio;

var builder = WebApplication.CreateBuilder(args);
var accountSid = "ACb5cb05bbff0bfd2c02b7a632313c5708";
var authToken = "c1b8bf130e942b294c0ffff3f2c417e3";

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