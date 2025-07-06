using DevOpsAssistant.Domain.Entities;

namespace DevOpsAssistant.Application.UseCases.PullRequests;

public interface IMergePullRequestUseCase
{
    Task<string> ExecuteAsync(GitRepository repo, string pullRequestName, string mergeMethod = "merge");
}