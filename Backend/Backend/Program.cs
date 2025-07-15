
using Backend.MapperConfiguration;
using Backend.Repository.Auth;
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPI_ITI_DB.Models;


namespace WebAPI_ITI_DB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string textForCorss = "";
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers().AddNewtonsoftJson(option=>
            {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            builder.Services.AddOpenApi();
            //builder.Services.AddScoped<UnitOfWork>();

            builder.Services.AddDbContext<dbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IAuthRepository, AuthRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddAutoMapper(typeof(mapperConfig));

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var config = builder.Configuration;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = config["Jwt:Issuer"],
                        ValidAudience = config["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
                    };
                });

            builder.Services.AddAuthorization();

            builder.Services.AddCors(option =>
            {
                option.AddPolicy(textForCorss,
                    builder =>
                    {
                        //Incase of a known host and you need to specify it
                        //builder.WithOrigins("https://localhost:7707", "https://localhost:3030",.....);
                        //OR Allow all origin

                        builder.AllowAnyOrigin();
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                    });
            });


            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(op => op.SwaggerEndpoint("/openapi/v1.json", "v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(textForCorss);

            app.MapControllers();

            app.Run();
        }
    }
}
