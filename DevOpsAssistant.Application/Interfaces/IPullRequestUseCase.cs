using DevOpsAssistant.Domain.Entities;

namespace DevOpsAssistant.Application.Interfaces;

public interface IPullRequestUseCase
{
    Task<IEnumerable<string>> ExecuteAsync(GitRepository repo);
}
