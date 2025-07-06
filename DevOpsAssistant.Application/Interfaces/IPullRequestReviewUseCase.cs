using DevOpsAssistant.Domain.Entities;

namespace DevOpsAssistant.Application.Interfaces;

public interface IPullRequestReviewUseCase
{
    Task<string> ExecuteAsync(GitRepository repo, string pullrequest);
}
