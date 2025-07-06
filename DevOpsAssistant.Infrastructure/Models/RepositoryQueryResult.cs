using Octokit.GraphQL;
using Octokit.GraphQL.Core;

namespace DevOpsAssistant.Infrastructure.Models;

public class RepositoryQueryResult
{
    public ID Id { get; set; }
    public BaseRefResult BaseRef { get; set; }
}
