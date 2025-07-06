using DevOpsAssistant.Application.Interfaces;
using DevOpsAssistant.Domain.Entities;

namespace DevOpsAssistant.Application.UsesCases;

public class ListOfWorkFlowsUseCase(IDevOpsService devOps) : IListOfWorkFlowsUseCase
{
    public async Task<IEnumerable<string>> ExecuteAsync(GitRepository repo)
    {
        return await devOps.GetListOfWorkFlow(repo);
    }
}
