using Mavericks_Bank.Context;
using Mavericks_Bank.Interfaces;
using Mavericks_Bank.Models;
using Mavericks_Bank.Repositories;
using Mavericks_Bank.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Mavericks_Bank
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                            },
                            new string[]{}
                        }
                    });
            });

            #region Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(options =>
                  {
                      options.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuerSigningKey = true,
                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SecretKey"])),
                          ValidateIssuer = false,
                          ValidateAudience = false
                      };
                  });


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MavericksBankPolicy", opts =>
                {
                    opts.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
                });
            });

            builder.Services.AddDbContext<MavericksBankContext>(opts =>
            {
                opts.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnectionString"));
            });
            #endregion

            #region Dependecy Injection
            builder.Services.AddScoped<IRepository<string,Validation>,ValidationRepository>();
            builder.Services.AddScoped<IRepository<int, Customers>, CustomersRepository>();
            builder.Services.AddScoped<IRepository<int, BankEmployees>, BankEmployeesRepository>();
            builder.Services.AddScoped<IRepository<int, Admin>, AdminRepository>();
            builder.Services.AddScoped<IRepository<int, Banks>, BanksRepository>();
            builder.Services.AddScoped<IRepository<int, Branches>, BranchesRepository>();
            builder.Services.AddScoped<IRepository<int, Accounts>, AccountsRepository>();
            builder.Services.AddScoped<IRepository<int, Beneficiaries>, BeneficiariesRepository>();
            builder.Services.AddScoped<IRepository<int, Loans>, LoansRepository>();
            builder.Services.AddScoped<IRepository<int, Transactions>, TransactionsRepository>();
            builder.Services.AddScoped<IRepository<int, AppliedLoans>, AppliedLoansRepository>();

            builder.Services.AddScoped<IBanksAdminService, BanksService>();
            builder.Services.AddScoped<IBranchesAdminService, BranchesService>();
            builder.Services.AddScoped<IValidationAdminService, ValidationService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<ICustomersAdminService, CustomersService>();
            builder.Services.AddScoped<IBankEmployeesAdminService, BankEmployeesService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<ILoansAdminService, LoansService>();
            builder.Services.AddScoped<IAccountsAdminService, AccountsService>();
            builder.Services.AddScoped<IBeneficiariesAdminService, BeneficiariesService>();
            builder.Services.AddScoped<ITransactionsAdminService, TransactionsService>();
            builder.Services.AddScoped<IAppliedLoansAdminService, AppliedLoansService>();
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("MavericksBankPolicy");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
