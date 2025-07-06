using DevOpsAssistant.Domain.Entities;

namespace DevOpsAssistant.Application.Interfaces;

public interface IAddCommentToPullRequestUseCase
{
    Task<string> ExecuteAsync(GitRepository repo, string pullRequestName, string comment);
}