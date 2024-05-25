using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
    var signinkey = System.Text.Encoding.UTF32
        .GetBytes("weyuyweuguyfdsgfuyds82357428726445326shbcdfsjyueuysefg");

        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = "Blog.API",
            ValidAudience = "Blog.Clint",
            ValidateIssuer = true,
            ValidateAudience = true,

            IssuerSigningKey = new SymmetricSecurityKey(signinkey),
            
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
