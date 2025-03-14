﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StarMart.Infrastructure.Contracts;
using StarMart.Infrastructure.Database;
using StarMart.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StarMart.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            AutoRegisterRepositories(services);

            return services;
        }

        private static void AutoRegisterRepositories(IServiceCollection services)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();
            IEnumerable<Type> repositoryInterfaces = types.Where(t => t.IsInterface && t.Name.ToLower().Trim().EndsWith("repository"));

            if (repositoryInterfaces.Any())
            {
                foreach (Type repositoryInterface in repositoryInterfaces)
                {
                    Type implementationType = types.FirstOrDefault(t =>
                        t.IsClass
                        && !t.IsAbstract
                        && !t.IsInterface
                        && repositoryInterface.IsAssignableFrom(t));

                    if (implementationType != null)
                    {
                        services.AddScoped(repositoryInterface, implementationType);
                    }
                }
            }
        }
    }
}
