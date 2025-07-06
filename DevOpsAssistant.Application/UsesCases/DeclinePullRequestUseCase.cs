using DevOpsAssistant.Application.Interfaces;
using DevOpsAssistant.Domain.Entities;

namespace DevOpsAssistant.Application.UseCases.PullRequests;

public class DeclinePullRequestUseCase(IRepositoryService repositoryService) : IDeclinePullRequestUseCase
{
    public async Task<string> ExecuteAsync(GitRepository repo, string pullRequestName)
    {
        return await repositoryService.DeclinePullRequestAsync(repo, pullRequestName);
    }
}