namespace DevOpsAssistant.Domain.Entities;

public record GitRepository(string Owner, string Name)
{
    public string FullName => $"{Owner}/{Name}";

    public static GitRepository Parse(string repository)
    {
        if (string.IsNullOrWhiteSpace(repository))
        {
            throw new ArgumentException("Repository cannot be null or empty.", nameof(repository));
        }
        var parts = repository.Split('/');
        if (parts.Length != 2)
        {
            throw new ArgumentException("Repository must be in the format 'owner/name'.", nameof(repository));
        }
        return new GitRepository(parts[0].Trim(), parts[1].Trim());
    }
}
