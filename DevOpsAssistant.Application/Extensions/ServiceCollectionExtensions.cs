using DevOpsAssistant.Application.Interfaces;
using DevOpsAssistant.Application.UseCases.PullRequests;
using DevOpsAssistant.Application.UsesCases;
using Microsoft.Extensions.DependencyInjection;

namespace DevOpsAssistant.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<ICreateBranchUseCase, CreateBranchUseCase>();
        services.AddSingleton<ITriggerWorkFlowUseCase, TriggerWorkFlowUseCase>();
        services.AddSingleton<IListOfWorkFlowsUseCase, ListOfWorkFlowsUseCase>();
        services.AddSingleton<IListOfPublicRepositoriesUseCase, ListOfPublicRepositoriesUseCase>();
        services.AddSingleton<IPullRequestUseCase, PullRequestUseCase>();
        services.AddSingleton<IPullRequestReviewUseCase, PullRequestReviewUseCase>();

        services.AddSingleton<IAddCommentToPullRequestUseCase, AddCommentToPullRequestUseCase>();
        services.AddSingleton<IDeclinePullRequestUseCase, DeclinePullRequestUseCase>();
        services.AddSingleton<IMergePullRequestUseCase, MergePullRequestUseCase>();

        return services;
    }
}
