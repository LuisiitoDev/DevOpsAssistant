using DevOpsAssistant.Application.Interfaces;
using DevOpsAssistant.Domain.Entities;
using DevOpsAssistant.Domain.Enums;

namespace DevOpsAssistant.Application.UsesCases;

public class TriggerWorkFlowUseCase(IDevOpsService devOps) : ITriggerWorkFlowUseCase
{
    public async Task<string> ExecuteAsync(GitRepository repo, string branch, string workflow)
    {
        return await devOps.TriggerWorkFlow(repo, branch, workflow);
    }
}
