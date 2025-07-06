using DevOpsAssistant.Web.DTOs;
using DevOpsAssistant.Web.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;

namespace DevOpsAssistant.Web.Components.Pages;

public partial class Chat : Microsoft.AspNetCore.Components.ComponentBase
{
    [Inject]
    protected IDevOpsAssitantRestService DevOps { get; set; }

    private List<ChatMessage> messages = new();
    private bool isTyping = false;
    private MessageInput messageInput { get; set; } = new();

    public class ChatMessage
    {
        public string Content { get; set; } = string.Empty;
        public bool IsUser { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }

    public class MessageInput
    {
        [Required(ErrorMessage = "Please enter a message")]
        [StringLength(1000, ErrorMessage = "Message cannot exceed 1000 characters")]
        public string Content { get; set; } = string.Empty;
    }

    private async Task SendMessage()
    {
        if (string.IsNullOrWhiteSpace(messageInput.Content))
            return;

        messages.Add(new ChatMessage
        {
            Content = messageInput.Content,
            IsUser = true,
            Timestamp = DateTime.Now
        });

        isTyping = true;

        var userMessage = messageInput.Content;
        messageInput.Content = string.Empty;

        StateHasChanged();
        await ScrollToBottom();

        

        var response = await DevOps.SendMessage(new ChatRequest
        {
            Message = userMessage,
            UserId = "LuisiitoDev"
        }, CancellationToken.None);

        StateHasChanged();

        messages.Add(new ChatMessage
        {
            Content = response.Message,
            IsUser = false,
            Timestamp = DateTime.Now
        });

        isTyping = false;
        StateHasChanged();
        await ScrollToBottom();
    }

    private string GenerateAssistantResponse(string userMessage)
    {
        var responses = new[]
        {
            $"I understand you're asking about: {userMessage}. How can I help you with this DevOps task?",
            $"That's an interesting question about {userMessage}. Let me assist you with that.",
            $"I can help you with {userMessage}. What specific aspect would you like to know more about?",
            $"Great question! Regarding {userMessage}, here's what I can tell you..."
        };

        return responses[new Random().Next(responses.Length)];
    }

    private async Task ScrollToBottom()
    {
        await JSRuntime.InvokeVoidAsync("scrollToBottom", "chatMessages");
    }
}
