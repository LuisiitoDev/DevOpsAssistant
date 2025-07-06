using DevOpsAssistant.Application.Interfaces;
using DevOpsAssistant.Infrastructure.Configuration;
using DevOpsAssistant.Infrastructure.SemanticKernel;
using DevOpsAssistant.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Octokit;

namespace DevOpsAssistant.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<GitHubOptions>(configuration.GetSection(nameof(GitHubOptions)).Bind);
        services.Configure<SemanticKernelOptions>(configuration.GetSection(nameof(SemanticKernelOptions)).Bind);

        services.AddSingleton<IRepositoryService, GitHubRepositoryService>();
        services.AddSingleton<IDevOpsService, DevOpsService>();
        services.AddSingleton<DevOpsKernelPlugin>();

        services.AddSingleton<ISemanticKernelService, SemanticKernelService>();


        services.AddSingleton(sp =>
        {
            var skOptions = sp.GetRequiredService<IOptions<SemanticKernelOptions>>().Value;
            var skPlugin = sp.GetRequiredService<DevOpsKernelPlugin>();
            var builder = Kernel.CreateBuilder()
            .AddAzureOpenAIChatCompletion(
                    skOptions.DeploymentName,
                    skOptions.Endpoint,
                    skOptions.ApiKey
                );

            var kernel = builder.Build();

            kernel.Plugins.AddFromObject(skPlugin, nameof(DevOpsKernelPlugin));

            return kernel;
        });

        services.AddSingleton(sp =>
        {
            var kernel = sp.GetRequiredService<Kernel>();
            return kernel.GetRequiredService<IChatCompletionService>();
        });

        services.AddSingleton(sp =>
        {
            var options = sp.GetRequiredService<IOptions<GitHubOptions>>().Value;

            var header = new ProductHeaderValue(options.AppName, options.AppVersion);

            var client = new GitHubClient(header);

            client.Credentials = new Credentials(options.Token);

            client.SetRequestTimeout(TimeSpan.FromSeconds(30));

            return client;
        });

        return services;
    }
}
