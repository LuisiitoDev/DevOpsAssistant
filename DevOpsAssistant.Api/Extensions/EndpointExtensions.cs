using DevOpsAssistant.Api.DTOs;
using DevOpsAssistant.Api.Handlers;

namespace DevOpsAssistant.Api.Extensions;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapDevOpsEndpoints(this IEndpointRouteBuilder endpoint)
    {
        var deveopsgroup = endpoint.MapGroup("api/v1/devops")
            .WithTags("DevOps")
            .WithOpenApi();

        deveopsgroup.MapPost("chat", ChatHandler.ExecuteAsync)
            .WithName("ProcessMessage")
            .WithSummary("Process a chat message with the DevOps assistant")
            .WithDescription("Send a message to the  DevOps assistant  and get an AI-powered response with function execution")
            .Produces<ChatResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status500InternalServerError);

        return endpoint;
    }
}
