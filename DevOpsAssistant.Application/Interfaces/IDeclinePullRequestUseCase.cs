using DevOpsAssistant.Domain.Entities;

namespace DevOpsAssistant.Application.Interfaces;

public interface IDeclinePullRequestUseCase
{
    Task<string> ExecuteAsync(GitRepository repo, string pullRequestName);
}