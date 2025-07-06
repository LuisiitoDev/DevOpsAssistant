namespace DevOpsAssistant.Application.Interfaces;

public interface ISemanticKernelService
{
    Task<string> ProcessMessageAsync(string message, string userId, CancellationToken cancellationToken);
}
