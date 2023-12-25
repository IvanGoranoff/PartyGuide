using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PartyGuide.DataAccess.DbContext;
using PartyGuide.DataAccess.DbManagers;
using PartyGuide.DataAccess.Interfaces;
using PartyGuide.Domain.Interfaces;
using PartyGuide.Domain.Managers;
using PartyGuide.Infrastructure.Services.GeoNames;

internal class Program
{
	private static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
		builder.Services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(connectionString));
		builder.Services.AddDatabaseDeveloperPageExceptionFilter();

		builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
			.AddRoles<IdentityRole>()
			.AddEntityFrameworkStores<ApplicationDbContext>();
		builder.Services.AddControllersWithViews();

		builder.Services.AddScoped<IServiceManager, ServiceManager>();
		builder.Services.AddScoped<IServiceDbManager, ServiceDbManager>();
		builder.Services.AddScoped<IRatingManager, RatingManager>();
		builder.Services.AddScoped<IRatingDbManager, RatingDbManager>();
		builder.Services.AddScoped<IGeoNamesService, GeoNamesService>();

		builder.Services.ConfigureApplicationCookie(options =>
		{
			options.Events = new CookieAuthenticationEvents
			{
				OnSignedIn = context =>
				{
					var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
					var user = userManager.GetUserAsync(context.Principal).Result;

					// Set the default role when the user signs in
					userManager.AddToRoleAsync(user, "User").Wait();

					return Task.CompletedTask;
				}
			};
		});


		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseMigrationsEndPoint();
		}
		else
		{
			app.UseExceptionHandler("/Service/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthorization();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Service}/{action=Index}/{id?}");
		app.MapRazorPages();

		// Seeding
		using (var scope = app.Services.CreateScope())
		{
			var roleManager =
				scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			var roles = new[] { "Admin", "User", "Guest" };

			foreach (var role in roles)
			{
				if (!await roleManager.RoleExistsAsync(role))
					await roleManager.CreateAsync(new IdentityRole(role));
			}
		}

		using (var scope = app.Services.CreateScope())
		{
			var userManager =
				scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

			string email = "administrator@admin.com";
			string password = "Test1234,";

			if (await userManager.FindByEmailAsync(email) == null)
			{
				var user = new IdentityUser();
				user.UserName = email;
				user.Email = email;
				//user.EmailConfirmed = true;

				await userManager.CreateAsync(user, password);

				await userManager.AddToRoleAsync(user, "Admin");
			}
		}

		app.Run();
	}
}