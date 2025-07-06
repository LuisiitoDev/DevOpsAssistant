using DevOpsAssistant.Application.Interfaces;
using DevOpsAssistant.Domain.Entities;

namespace DevOpsAssistant.Application.UsesCases;

public class CreateBranchUseCase(IRepositoryService repositoryService) : ICreateBranchUseCase
{
    public async Task<string> ExecuteAsync(GitRepository repo, string name, string @base)
    {
        return await repositoryService.CreateBranchAsync(repo, name, @base);
    }
}
