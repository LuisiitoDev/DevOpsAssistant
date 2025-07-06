using DevOpsAssistant.Application.Interfaces;
using DevOpsAssistant.Domain.Entities;

namespace DevOpsAssistant.Application.UsesCases;

public class PullRequestUseCase(IRepositoryService repository) : IPullRequestUseCase
{
    public async Task<IEnumerable<string>> ExecuteAsync(GitRepository repo)
    {
        return await repository.GetPullRequests(repo);
    }
}
