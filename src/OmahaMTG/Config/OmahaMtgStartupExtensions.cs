using Microsoft.Extensions.DependencyInjection;
//using OmahaMTG.Content.Host;
//using OmahaMTG.Content.Sponsor;
using System.Collections.Generic;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
//using OmahaMTG.Accessors.UserGroupAccessor;
//using OmahaMTG.Api;
//using OmahaMTG.Content.Meeting;
//using OmahaMTG.Content.Post;
//using OmahaMTG.Content.Presentation;
//using OmahaMTG.Content.Presenter;
using OmahaMTG.Data;


namespace OmahaMTG.Config
{
    public static class OmahaMtgStartupExtensions
    {
        public static IServiceCollection AddOmahaMtgContent(this IServiceCollection services, OmahaMtgConfig config)
        {
            services.AddDbContext<Data.UserGroupContext>(options =>
                options.UseSqlServer(
                    config.OmahaMtgDbConnectionString));

            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }

        public static IApplicationBuilder UseOmahaMtgContent(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<Data.UserGroupContext>())
                {
                    //  context.Database.Migrate();
                    context.Database.EnsureCreated();
                }
            }

            //app.UseAuthentication();

            return app;
        }

    }
}