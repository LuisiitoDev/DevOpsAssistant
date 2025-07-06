using DevOpsAssistant.Application.Interfaces;
using DevOpsAssistant.Domain.Entities;

namespace DevOpsAssistant.Application.UseCases.PullRequests;

public class MergePullRequestUseCase(IRepositoryService repositoryService) : IMergePullRequestUseCase
{
    public async Task<string> ExecuteAsync(GitRepository repo, string pullRequestName, string mergeMethod = "merge")
    {
        return await repositoryService.MergePullRequestAsync(repo, pullRequestName, mergeMethod);
    }
}