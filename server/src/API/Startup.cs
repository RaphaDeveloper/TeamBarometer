using API.DomainEventHandlers;
using API.Hubs;
using Application.Sessions.UseCases;
using Domain.TeamBarometer.Events;
using Domain.TeamBarometer.Repositories;
using Domain.TeamBarometer.UseCases;
using DomainEventManager;
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
			services.AddScoped<MeetingService>();
			services.AddSingleton<InMemoryMeetingRepository>();
			services.AddSingleton<TemplateQuestionRepository, InMemoryTemplateQuestionRepository>();
			services.AddSingleton<SessionHub>();
			services.AddSingleton<RefreshSession>();

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
			DomainEvent.Bind<WhenTheQuestionIsEnabled, RefreshSession>(app.ApplicationServices);
			DomainEvent.Bind<WhenAllUsersAnswerTheQuestion, RefreshSession>(app.ApplicationServices);

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
