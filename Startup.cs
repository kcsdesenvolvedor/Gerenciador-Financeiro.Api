using FireSharp;
using FireSharp.Interfaces;
using FluentMigrator.Runner;
using Gerenciador_Financeiro.Business.Services.Service;
using Gerenciador_Financeiro.Domains.Domains.Balance.Repository;
using Gerenciador_Financeiro.Domains.Domains.Balance.Service;
using Gerenciador_Financeiro.Domains.Domains.Demostrative.Repository;
using Gerenciador_Financeiro.Domains.Domains.Demostrative.Service;
using Gerenciador_Financeiro.Domains.Domains.Expense.Repository;
using Gerenciador_Financeiro.Domains.Domains.Expense.Service;
using Gerenciador_Financeiro.Domains.Domains.Revenue.Repository;
using Gerenciador_Financeiro.Domains.Domains.Revenue.Service;
using Gerenciador_Financeiro.Domains.Domains.User;
using Gerenciador_Financeiro.Domains.Domains.User.Repository;
using Gerenciador_Financeiro.Domains.Domains.User.Service;
using Gerenciador_Financeiro.Infra;
using Gerenciador_Financeiro.Infra.Repositories;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Gerenciador_Financeiro.API
{
    public class Startup
    {
        private string connection;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

            RegisterInterfaces(services);
            connection = Configuration["ConexaoSqlite:SqliteConnectionString"];
            InjectionDb(services);
        }

        public void InjectionDb(IServiceCollection services)
        {
            services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    .AddSQLite()
                    // Set the connection string
                    .WithGlobalConnectionString(connection)
                    // Define the assembly containing the migrations
                    .ScanIn(Assembly.GetExecutingAssembly()).For.All())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole());
            // Build the service provider
        }

        private void RegisterInterfaces(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IExpenseService, ExpenseService>();
            services.AddTransient<IExpenseRepository, ExpenseRepository>();
            services.AddTransient<IRevenueService, RevenueService>();
            services.AddTransient<IRevenueRepository, RevenueRepository>();
            services.AddTransient<IDemonstrativeService, DemonstrativeService>();
            services.AddTransient<IDemonstrativeRepository, DemonstrativeRepository>();
            services.AddTransient<IBalanceService, BalanceService>();
            services.AddTransient<IBalanceRepository, BalanceRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
