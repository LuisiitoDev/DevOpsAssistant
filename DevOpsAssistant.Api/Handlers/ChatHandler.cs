using DevOpsAssistant.Api.DTOs;
using DevOpsAssistant.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevOpsAssistant.Api.Handlers;

public static class ChatHandler
{
    public static async Task<IResult> ExecuteAsync([FromBody] ChatRequest request, [FromServices] ISemanticKernelService kernelService, CancellationToken cancellationToken)
    {
        var response = await kernelService.ProcessMessageAsync(request.Message, request.UserId, cancellationToken);

        return Results.Ok(new ChatResponse
        {
            Message = response,
            UserId = request.UserId
        });
    }
}
