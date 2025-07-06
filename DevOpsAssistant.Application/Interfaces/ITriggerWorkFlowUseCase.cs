using DevOpsAssistant.Domain.Entities;

namespace DevOpsAssistant.Application.Interfaces;

public interface ITriggerWorkFlowUseCase
{
    Task<string> ExecuteAsync(GitRepository repo, string branch, string workflow);
}
