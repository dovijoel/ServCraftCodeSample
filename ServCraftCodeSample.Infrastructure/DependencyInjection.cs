using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using ServCraftCodeSample.Core.Services;
using ServCraftCodeSample.Infrastructure.Data;
using ServCraftCodeSample.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServCraftCodeSample.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ChatDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("servcraftcodesampledb")));

            var openAiSettings = configuration.GetRequiredSection("OpenAi");

            services.AddOpenAIChatCompletion(
                modelId: openAiSettings["ModelId"],
                apiKey: openAiSettings["ApiKey"],
                orgId: openAiSettings["OrgId"]
            );

            services.AddTransient((serviceProvider) => {
                return new Kernel(serviceProvider);
            });

            services.AddScoped<IChatService, ChatService>();
            return services;
        }
    }
}
