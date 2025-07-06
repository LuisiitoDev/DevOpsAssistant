using DevOpsAssistant.Web.DTOs;
using Refit;

namespace DevOpsAssistant.Web.Interfaces;

public interface IDevOpsAssitantRestService
{
    [Post("/api/v1/devops/chat")]
    Task<ChatResponse> SendMessage(ChatRequest request, CancellationToken cancellationToken);
}
