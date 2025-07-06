using System.ComponentModel.DataAnnotations;

namespace DevOpsAssistant.Web.DTOs;

public class ChatRequest
{
    [Required, MaxLength(1)] public string Message { get; set; } = string.Empty;
    [Required, MaxLength(1)] public string UserId { get; set; } = "LuisiitoDev";
}
