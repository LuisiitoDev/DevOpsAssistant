using DevOpsAssistant.Application.Interfaces;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace DevOpsAssistant.Infrastructure.SemanticKernel;

public class SemanticKernelService : ISemanticKernelService
{
    private readonly Kernel _kernel;
    private readonly IChatCompletionService _chat;

    private readonly ChatHistory _history = [];
    private readonly string[] _authorizedUsers = { "LuisiitoDev" };

    public SemanticKernelService(Kernel kernel, IChatCompletionService chat)
    {
        _kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
        _chat = chat ?? throw new ArgumentNullException(nameof(chat));

        _history.AddAssistantMessage(PromptBuilder.BuildSystemPrompt());
    }

    public async Task<string> ProcessMessageAsync(string message, string userId, CancellationToken cancellationToken)
    {
        if (!IsAuthorizedUser(userId)) return "❌ Only LuisiitoDev can use this DevOps assistant.";

        var userPrompt = PromptBuilder.BuildUserPrompt(message, userId);
        _history.AddUserMessage(userPrompt);

        var result = await _chat.GetChatMessageContentsAsync(_history, executionSettings: new OpenAIPromptExecutionSettings()
        {
            FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(),
        }, kernel: _kernel, cancellationToken: cancellationToken);

        return string.Join(Environment.NewLine, result);
    }

    private bool IsAuthorizedUser(string userId)
    {
        return _authorizedUsers.Contains(userId, StringComparer.OrdinalIgnoreCase);
    }
}
