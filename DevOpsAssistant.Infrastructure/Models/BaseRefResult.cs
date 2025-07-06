using Octokit.GraphQL;

namespace DevOpsAssistant.Infrastructure.Models;

public class BaseRefResult
{
    public ID Id { get; set; }
    public TargetResult Target { get; set; }
}
