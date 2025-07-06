using DevOpsAssistant.Domain.Entities;

namespace DevOpsAssistant.Application.Interfaces;

public interface IListOfWorkFlowsUseCase
{
    Task<IEnumerable<string>> ExecuteAsync(GitRepository repo);
}
