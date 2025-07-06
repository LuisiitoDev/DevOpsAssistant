using DevOpsAssistant.Application.Interfaces;
using DevOpsAssistant.Application.UseCases.PullRequests;
using DevOpsAssistant.Domain.Entities;
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace DevOpsAssistant.Infrastructure.SemanticKernel;

public class DevOpsKernelPlugin(
    ICreateBranchUseCase createBranchUseCase,
    ITriggerWorkFlowUseCase triggerWorkFlowUseCase,
    IListOfWorkFlowsUseCase listOfWorkFlowsUseCase,
    IListOfPublicRepositoriesUseCase listOfPublicRepositoriesUseCase,
    IPullRequestUseCase pullRequestUseCase,
    IPullRequestReviewUseCase pullRequestReview,
    IAddCommentToPullRequestUseCase addCommentToPullRequestUseCase,
    IDeclinePullRequestUseCase declinePullRequestUseCase,
    IMergePullRequestUseCase mergePullRequestUseCase)
{
    [KernelFunction(nameof(CreateBranch))]
    [Description("Creates a new branch in repository")]
    public async Task<string> CreateBranch(
        [Description("Repository in format 'owner/repo'")] string repository,
        [Description("Name of the new branch")] string branchName,
        [Description("Name of the base branch")] string baseBranch = "main")
    {
        var repo = GitRepository.Parse(repository);
        return await createBranchUseCase.ExecuteAsync(repo, branchName, baseBranch);
    }

    [KernelFunction(nameof(TriggerWorkflow))]
    [Description("Triggers the existing workflow in the repository")]
    public async Task<string> TriggerWorkflow(
        [Description("Repository in format 'owner/repo'")] string repository,
        [Description("Branch to run workdlow on")] string branch,
        [Description("Workflor to trigger ")] string workflow)
    {
        var repo = GitRepository.Parse(repository);

        return await triggerWorkFlowUseCase.ExecuteAsync(repo, branch, workflow);
    }

    [KernelFunction(nameof(GetListOfWorkFlows))]
    [Description("Get list of workflows in the repository")]
    public async Task<IEnumerable<string>> GetListOfWorkFlows(
        [Description("Repository in format 'owner/repo'")] string repository)
    {
        var repo = GitRepository.Parse(repository);
        return await listOfWorkFlowsUseCase.ExecuteAsync(repo);
    }

    [KernelFunction(nameof(GetListOfRepositories))]
    [Description("Get list of repositories")]
    public async Task<IEnumerable<string>> GetListOfRepositories()
    {
        return await listOfPublicRepositoriesUseCase.ExecuteAsync();
    }

    [KernelFunction(nameof(GetPullRequests))]
    [Description("Get list of pull request in the repository")]
    public async Task<IEnumerable<string>> GetPullRequests(
        [Description("Repository in format 'owner/repo'")] string repository)
    {
        var repo = GitRepository.Parse(repository);
        return await pullRequestUseCase.ExecuteAsync(repo);
    }

    [KernelFunction(nameof(GetPullRequest))]
    [Description("Get info of a pull request in a repository")]
    public async Task<string> GetPullRequest(
    [Description("Repository in format 'owner/repo'")] string repository,
    [Description("Name of the pull request")] string pullRequest)
    {
        var repo = GitRepository.Parse(repository);
        return await pullRequestReview.ExecuteAsync(repo, pullRequest);
    }

    // Add Comment

    [KernelFunction(nameof(AddComment))]
    [Description("Add a comment to a pull request in the repository")]
    public async Task<string> AddComment(
        [Description("Repository in format 'owner/repo'")] string repository,
        [Description("Name of the pull request")] string pullRequest,
        [Description("Comment text")] string comment)
    {
        var repo = GitRepository.Parse(repository);
        return await addCommentToPullRequestUseCase.ExecuteAsync(repo, pullRequest, comment);
    }

    // Merge

    [KernelFunction(nameof(MergePullRequest))]
    [Description("Merge a pull request in the repository")]
    public async Task<string> MergePullRequest(
        [Description("Repository in format 'owner/repo'")] string repository,
        [Description("Name of the pull request")] string pullRequest)
    {
        var repo = GitRepository.Parse(repository);
        return await mergePullRequestUseCase.ExecuteAsync(repo, pullRequest);
    }

    // Decline

    [KernelFunction(nameof(DeclinePullRequest))]
    [Description("Decline a pull request in the repository")]
    public async Task<string> DeclinePullRequest(
        [Description("Repository in format 'owner/repo'")] string repository,
        [Description("Name of the pull request")] string pullRequest)
    {
        var repo = GitRepository.Parse(repository);
        return await declinePullRequestUseCase.ExecuteAsync(repo, pullRequest);
    }
}
