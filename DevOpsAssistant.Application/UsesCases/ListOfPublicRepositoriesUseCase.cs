using DevOpsAssistant.Application.Interfaces;

namespace DevOpsAssistant.Application.UsesCases;

public class ListOfPublicRepositoriesUseCase(IRepositoryService repository) : IListOfPublicRepositoriesUseCase
{
    public async Task<IEnumerable<string>> ExecuteAsync()
    {
        return await repository.GetListOfPublicRepositories();
    }
}
