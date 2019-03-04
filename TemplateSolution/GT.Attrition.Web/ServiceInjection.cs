using NA.Template.DataAccess.Interfaces;
using NA.Template.DataAccess.Repositories;
using NA.Template.Entities;
using NA.Template.Web.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NA.Template.Web
{
    public static class ServiceInjection
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<IUserStore<ApplicationUser>, UserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();

            services.AddTransient<IBaseRepository, BaseRepository>();
            services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddTransient<IClientDivisionRepository, ClientDivisionRepository>();

            return services;
        }
    }
}
