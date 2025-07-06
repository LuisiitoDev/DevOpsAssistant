using DevOpsAssistant.Domain.Entities;
using DevOpsAssistant.Domain.Enums;

namespace DevOpsAssistant.Application.Interfaces;

public interface IDevOpsService
{
    Task<string> TriggerWorkFlow(GitRepository repo, string branch, string workflowName);
    Task<IEnumerable<string>> GetListOfWorkFlow(GitRepository repo);
}
