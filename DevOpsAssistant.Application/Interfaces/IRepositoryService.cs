using DevOpsAssistant.Domain.Entities;

namespace DevOpsAssistant.Application.Interfaces;

public interface IRepositoryService
{
    Task<string> CreateBranchAsync(GitRepository repo, string name, string @base);
    Task<IEnumerable<string>> GetListOfPublicRepositories();
    Task<IEnumerable<string>> GetPullRequests(GitRepository repo);
    Task<string> GetPullRequest(GitRepository repo, string pullRequestName);
    Task<string> MergePullRequestAsync(GitRepository repo, string pullRequestName, string mergeMethod = "merge");
    Task<string> AddCommentToPullRequestAsync(GitRepository repo, string pullRequestName, string comment);
    Task<string> DeclinePullRequestAsync(GitRepository repo, string pullRequestName);
}
