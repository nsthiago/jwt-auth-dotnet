using jwt_auth_dotnet;
using jwt_auth_dotnet.Repositories;
using jwt_auth_dotnet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<ITokenService,TokenService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddControllers();


// First, we need to inform .NET that we are using authentication. To do this, we need to use the AddAuthentication method.
builder.Services.AddAuthentication(x =>
{
    // We need to specify the authentication type, in this case, JWT Bearer, and its authentication model (Challenge). In this case, both will have the value JwtBearer.
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    // Now, we need to configure how the application will validate the token.
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.PrivateKey))
        
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

// The last step is to inform .NET that we are using authentication and authorization. To do this, we need to call app.UseAuthentication() and app.UseAuthorization().
app.UseAuthentication();
app.UseAuthorization();

app.Run();
