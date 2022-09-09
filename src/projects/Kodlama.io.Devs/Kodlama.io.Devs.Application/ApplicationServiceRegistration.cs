using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Validation;
using Core.Security.JWT;
using FluentValidation;
using Kodlama.io.Devs.Application.Features.Developers.Rules;
using Kodlama.io.Devs.Application.Features.GitHubProfiles.Rules;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Rules;
using Kodlama.io.Devs.Application.Features.Technologies.Rules;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Kodlama.io.Devs.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<ProgrammingLanguageBusinessRules>();
            services.AddScoped<TechnologyBusinessRules>();
            services.AddScoped<DeveloperBusinessRules>();
            services.AddScoped<GitHubProfileBusinessRules>();


            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }
    }
}
