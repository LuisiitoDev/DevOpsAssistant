using DevOpsAssistant.Application.Interfaces;
using DevOpsAssistant.Domain.Entities;
using DevOpsAssistant.Domain.Enums;
using Octokit;

namespace DevOpsAssistant.Infrastructure.Services;

public class DevOpsService(GitHubClient gitHub) : IDevOpsService
{
    public async Task<string> TriggerWorkFlow(GitRepository repo, string branch, string workflowName)
    {
        var workflow = await gitHub.Actions.Workflows.Get(repo.Owner, repo.Name, workflowName);

        await gitHub.Actions.Workflows.CreateDispatch(repo.Owner, repo.Name, workflow.Id, new CreateWorkflowDispatch(branch));

        await Task.Delay(3000);

        var runs = await gitHub.Actions.Workflows.Runs.ListByWorkflow(repo.Owner, repo.Name, workflow.Id);
        var latestRun = runs.WorkflowRuns.OrderByDescending(r => r.CreatedAt).FirstOrDefault();

        return $"""
                    ✅ Existing workflow triggered successfully!
                    
                    📋 Workflow Details:
                    • Name: {workflow.Name}
                    • File: {workflow.Path}
                    • State: {workflow.State}
                    • Branch: {branch}
                    • Run Number: #{latestRun?.RunNumber ?? 0}
                    • Triggered by: LuisiitoDev
                    • Timestamp: 2025-07-06 17:15:22
                    
                    🔗 Workflow: https://github.com/{repo.Owner}/{repo.Name}/actions/workflows/{workflow.Path.Split('/').Last()}
                    🔗 Latest Run: {latestRun?.HtmlUrl ?? $"https://github.com/{repo.Owner}/{repo.Name}/actions"}
                    
                    💡 Monitor the workflow execution in GitHub Actions!
                    """;

    }

    public async Task<IEnumerable<string>> GetListOfWorkFlow(GitRepository repo)
    {
        var workflows = await gitHub.Actions.Workflows.List(repo.Owner, repo.Name);
        return [.. workflows.Workflows.Select(w => $"{w.Name} ({w.Path})")];
    }
}
