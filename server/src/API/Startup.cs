using API.Hubs;
using Application.Sessions.UseCases;
using Domain.Sessions.Repositories;
using Domain.Sessions.UseCases;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddSignalR();

			services.AddScoped<SessionAppService>();
			services.AddScoped<SessionService>();
			services.AddScoped<InMemorySessionRepository>();
			services.AddScoped<InMemoryTemplateQuestionRepository>();
			services.AddSingleton<SessionHub>();			

			services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
			{
				builder.SetIsOriginAllowed(_ => true)
					   .AllowAnyMethod()
					   .AllowAnyHeader()
					   .AllowCredentials();
			}));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseCors("CorsPolicy");

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapHub<SessionHub>("/sessionHub/{sessionId}");
			});
		}
	}
}
