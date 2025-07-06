namespace DevOpsAssistant.Application.Interfaces;

public interface IListOfPublicRepositoriesUseCase
{
    Task<IEnumerable<string>> ExecuteAsync();
}
