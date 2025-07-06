using DevOpsAssistant.Application.Interfaces;
using DevOpsAssistant.Domain.Entities;
using Octokit;

namespace DevOpsAssistant.Infrastructure.Services;

public class GitHubRepositoryService(GitHubClient github) : IRepositoryService
{
    public async Task<string> CreateBranchAsync(GitRepository repo, string name, string @base)
    {
        var baseBranch = await github.Git.Reference.Get(repo.Owner, repo.Name, $"heads/{@base}");
        if (baseBranch == null) return $"Base branch '{@base}' does not exist in repository '{repo.FullName}'.";

        var newReference = new NewReference($"refs/heads/{name}", baseBranch.Object.Sha);
        await github.Git.Reference.Create(repo.Owner, repo.Name, newReference);

        return $"✔ Branch '{repo.Name}' created successfully in {repo.FullName}";
    }

    public async Task<IEnumerable<string>> GetListOfPublicRepositories()
    {
        var result = await github.Repository.GetAllForCurrent();
        return result.Where(r => !r.Private).Select(r => r.FullName);
    }

    public async Task<IEnumerable<string>> GetPullRequests(GitRepository repo)
    {
        var pullRequests = await github.PullRequest.GetAllForRepository(repo.Owner, repo.Name);
        return pullRequests.Select(pr => pr.Title);
    }

    public async Task<string> GetPullRequest(GitRepository repo, string pullRequestName)
    {
        var pullRequests = await github.PullRequest.GetAllForRepository(repo.Owner, repo.Name);
        var pullRequest = pullRequests.FirstOrDefault(pr => pr.Title.Equals(pullRequestName, StringComparison.OrdinalIgnoreCase));

        if (pullRequest == null)
        {
            return $"Pull request '{pullRequestName}' not found in repository '{repo.FullName}'.";
        }

        // Get the files that were modified in this PR
        var prFiles = await github.PullRequest.Files(repo.Owner, repo.Name, pullRequest.Number);

        // Format the modified files for AI review
        var filesForReview = $"""
        # Code Review Request: {pullRequest.Title}
        
        **Repository:** {repo.FullName}
        **Author:** {pullRequest.User.Login}
        **Files Modified:** {prFiles.Count}
        **Total Changes:** +{prFiles.Sum(f => f.Additions)} -{prFiles.Sum(f => f.Deletions)}
        
        ## Files to Review:
        
        {string.Join("\n\n", prFiles.Select(file => $"""
        ### 📄 {file.FileName}
        **Status:** {file.Status}
        **Changes:** +{file.Additions} -{file.Deletions}
        
        ```diff
        {file.Patch ?? "Binary file or no changes to display"}
        ```
        """))}
        
        ## Review Instructions:
        Please analyze each modified file for:
        - Code quality and best practices
        - Potential bugs or logic errors
        - Security vulnerabilities
        - Performance implications
        - Code maintainability and readability
        - Compliance with coding standards
        
        Focus on the actual changes (lines with + and - prefixes) and their impact.
        """;

        return filesForReview;
    }

    public async Task<string> MergePullRequestAsync(GitRepository repo, string pullRequestName, string mergeMethod = "merge")
    {
        var pullRequests = await github.PullRequest.GetAllForRepository(repo.Owner, repo.Name);
        var pullRequest = pullRequests.FirstOrDefault(pr => pr.Title.Equals(pullRequestName, StringComparison.OrdinalIgnoreCase));

        if (pullRequest == null)
        {
            return $"Pull request '{pullRequestName}' not found in repository '{repo.FullName}'.";
        }

        if (pullRequest.State != ItemState.Open)
        {
            return $"Pull request '{pullRequestName}' is not open and cannot be merged.";
        }

        var mergePullRequest = new MergePullRequest
        {
            CommitTitle = $"Merge pull request #{pullRequest.Number}",
            MergeMethod = mergeMethod switch
            {
                "squash" => PullRequestMergeMethod.Squash,
                "rebase" => PullRequestMergeMethod.Rebase,
                _ => PullRequestMergeMethod.Merge
            }
        };

        try
        {
            await github.PullRequest.Merge(repo.Owner, repo.Name, pullRequest.Number, mergePullRequest);
            return $"✔ Pull request '{pullRequestName}' merged successfully in {repo.FullName}";
        }
        catch (Exception ex)
        {
            return $"❌ Failed to merge pull request '{pullRequestName}': {ex.Message}";
        }
    }

    public async Task<string> AddCommentToPullRequestAsync(GitRepository repo, string pullRequestName, string comment)
    {
        var pullRequests = await github.PullRequest.GetAllForRepository(repo.Owner, repo.Name);
        var pullRequest = pullRequests.FirstOrDefault(pr => pr.Title.Equals(pullRequestName, StringComparison.OrdinalIgnoreCase));

        if (pullRequest == null)
        {
            return $"Pull request '{pullRequestName}' not found in repository '{repo.FullName}'.";
        }

        try
        {
            await github.Issue.Comment.Create(repo.Owner, repo.Name, pullRequest.Number, comment);
            return $"✔ Comment added to pull request '{pullRequestName}' in {repo.FullName}";
        }
        catch (Exception ex)
        {
            return $"❌ Failed to add comment to pull request '{pullRequestName}': {ex.Message}";
        }
    }

    public async Task<string> DeclinePullRequestAsync(GitRepository repo, string pullRequestName)
    {
        var pullRequests = await github.PullRequest.GetAllForRepository(repo.Owner, repo.Name);
        var pullRequest = pullRequests.FirstOrDefault(pr => pr.Title.Equals(pullRequestName, StringComparison.OrdinalIgnoreCase));

        if (pullRequest == null)
        {
            return $"Pull request '{pullRequestName}' not found in repository '{repo.FullName}'.";
        }

        if (pullRequest.State != ItemState.Open)
        {
            return $"Pull request '{pullRequestName}' is already closed.";
        }

        try
        {
            var pullRequestUpdate = new PullRequestUpdate
            {
                State = ItemState.Closed
            };

            await github.PullRequest.Update(repo.Owner, repo.Name, pullRequest.Number, pullRequestUpdate);
            return $"✔ Pull request '{pullRequestName}' declined successfully in {repo.FullName}";
        }
        catch (Exception ex)
        {
            return $"❌ Failed to decline pull request '{pullRequestName}': {ex.Message}";
        }
    }

    public async Task<string> RequestChangesAsync(GitRepository repo, string pullRequestName, string reviewComment)
    {
        var pullRequests = await github.PullRequest.GetAllForRepository(repo.Owner, repo.Name);
        var pullRequest = pullRequests.FirstOrDefault(pr => pr.Title.Equals(pullRequestName, StringComparison.OrdinalIgnoreCase));

        if (pullRequest == null)
        {
            return $"Pull request '{pullRequestName}' not found in repository '{repo.FullName}'.";
        }

        try
        {
            var pullRequestReview = new PullRequestReviewCreate
            {
                Body = reviewComment,
                Event = PullRequestReviewEvent.RequestChanges
            };

            await github.PullRequest.Review.Create(repo.Owner, repo.Name, pullRequest.Number, pullRequestReview);
            return $"✔ Changes requested for pull request '{pullRequestName}' in {repo.FullName}";
        }
        catch (Exception ex)
        {
            return $"❌ Failed to request changes for pull request '{pullRequestName}': {ex.Message}";
        }
    }
}
