using DevOpsAssistant.Application.Interfaces;
using DevOpsAssistant.Domain.Entities;

namespace DevOpsAssistant.Application.UsesCases;

public class PullRequestReviewUseCase(IRepositoryService repository) : IPullRequestReviewUseCase
{
    public async Task<string> ExecuteAsync(GitRepository repo, string pullrequest)
    {
        return await repository.GetPullRequest(repo, pullrequest);
    }
}
