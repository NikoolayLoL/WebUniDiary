using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebUniDiary.Services;

namespace WebUniDiary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();

            builder.Services.AddLogging();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(40);
            });

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
                options.UseSqlServer(connectionString);
            });

            // Template for the Coureses that a given Semester has
            builder.Services.AddScoped<SemesterCourseService>();
            // Handles new Specialeties and Creates the nessecerry Semester lines
            builder.Services.AddScoped<SpecialtyBookService>();
            // Handles Students that are inserted in a Study path
            builder.Services.AddScoped<StudentBookService>();

            builder.Services.AddRazorPages()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.UseMiddleware<LoginMiddleware>();

            app.MapRazorPages();

            app.Run();
        }
    }
}
