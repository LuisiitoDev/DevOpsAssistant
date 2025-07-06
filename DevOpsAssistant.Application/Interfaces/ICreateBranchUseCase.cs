using DevOpsAssistant.Domain.Entities;

namespace DevOpsAssistant.Application.Interfaces;

public interface ICreateBranchUseCase
{
    Task<string> ExecuteAsync(GitRepository repo, string name, string @base);
}
