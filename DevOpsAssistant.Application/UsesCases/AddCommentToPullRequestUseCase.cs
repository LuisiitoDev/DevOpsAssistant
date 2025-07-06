using DevOpsAssistant.Application.Interfaces;
using DevOpsAssistant.Domain.Entities;

namespace DevOpsAssistant.Application.UseCases.PullRequests;

public class AddCommentToPullRequestUseCase(IRepositoryService repositoryService) : IAddCommentToPullRequestUseCase
{
    public async Task<string> ExecuteAsync(GitRepository repo, string pullRequestName, string comment)
    {
        return await repositoryService.AddCommentToPullRequestAsync(repo, pullRequestName, comment);
    }
}