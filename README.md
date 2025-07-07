# DevOps Assistant 🚀

![DevOps](https://github.com/user-attachments/assets/3f28ff5c-22db-4941-8dde-fad857aa8985)

## Overview

DevOps Assistant is a comprehensive solution designed to streamline and automate DevOps workflows. This AI-powered assistant helps teams manage repositories, pull requests, workflows, and other DevOps operations through a user-friendly interface and RESTful API.

## 🏗️ Architecture

This solution follows Clean Architecture principles with clear separation of concerns:

```
DevOpsAssistant/
├── 🌐 DevOpsAssistant.Api/          # REST API layer
├── 🎯 DevOpsAssistant.Application/  # Business logic & use cases
├── 🏛️ DevOpsAssistant.Domain/       # Domain entities & enums
├── 🔧 DevOpsAssistant.Infrastructure/ # External services & data access
└── 💻 DevOpsAssistant.Web/          # Blazor web application
```

## ✨ Features

### Core Capabilities
- **Repository Management**: List and manage public repositories
- **Pull Request Operations**: Create, merge, decline, and review pull requests
- **Branch Management**: Create and manage git branches
- **Workflow Automation**: Trigger and monitor GitHub Actions workflows
- **AI-Powered Chat**: Interact with DevOps operations through natural language

### Use Cases
- 📋 List public repositories
- 🔀 Create and manage branches
- 📝 Create and review pull requests
- 💬 Add comments to pull requests
- ✅ Merge pull requests
- ❌ Decline pull requests
- ⚡ Trigger GitHub workflows
- 📊 List available workflows

## 🛠️ Technology Stack

- **Backend**: .NET 9.0 / ASP.NET Core
- **Frontend**: Blazor Server
- **API Documentation**: OpenAPI/Swagger with Scalar
- **HTTP Client**: Refit
- **Architecture**: Clean Architecture
- **AI Integration**: Microsoft Semantic Kernel
- **DevOps Integration**: GitHub API (Octokit), GitHub GraphQL

## 🚀 Getting Started

### Prerequisites
- .NET 9.0 SDK
- Git
- GitHub account with appropriate permissions
- OpenAI API key for AI functionality

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd DevOpsAssistant
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Configure settings**
   Update `appsettings.json` files in both API and Web projects with your:
   - GitHub token (for repository access)
   - OpenAI API key (for AI functionality)
   - API endpoints
   - Other configuration values

4. **Run the API**
   ```bash
   cd DevOpsAssistant.Api
   dotnet run
   ```

5. **Run the Web Application**
   ```bash
   cd DevOpsAssistant.Web
   dotnet run
   ```

## 📡 API Endpoints

The API provides a conversational interface for all DevOps operations through AI-powered chat:

- `POST /api/v1/devops/chat` - Chat with the DevOps assistant (main endpoint)
- `GET /info` - API information and health status

### Available DevOps Operations (via Chat)
All DevOps operations are performed through natural language commands sent to the chat endpoint:

- 📋 **List repositories**: "Show me all repositories"
- 🔀 **Create branch**: "Create a new branch called feature/auth from main"
- 📊 **List workflows**: "What workflows are available in owner/repo?"
- ⚡ **Trigger workflow**: "Run the CI/CD pipeline on main branch"
- 📝 **List pull requests**: "Show me all pull requests in owner/repo"
- 👀 **Review pull request**: "Review pull request #123 in owner/repo"
- 💬 **Add comment**: "Add comment 'LGTM' to pull request #123"
- ✅ **Merge pull request**: "Merge pull request #123 in owner/repo"
- ❌ **Decline pull request**: "Decline pull request #123 in owner/repo"

## 🎯 Usage Examples

### Chat with DevOps Assistant
```json
POST /api/v1/devops/chat
{
  "message": "Create a new branch called feature/user-authentication from main in luisiitodev/my-repo",
  "userId": "developer123"
}
```

**Response:**
```json
{
  "message": "✅ Branch created successfully!\n\n📋 Branch Details:\n• Repository: luisiitodev/my-repo\n• Branch: feature/user-authentication\n• Created from: main\n• Status: Ready for development",
  "userId": "developer123"
}
```

### Trigger a Workflow
```json
POST /api/v1/devops/chat
{
  "message": "Trigger the ci-cd-pipeline workflow on main branch in luisiitodev/my-repo",
  "userId": "developer123"
}
```

### List Repositories
```json
POST /api/v1/devops/chat
{
  "message": "Show me all my repositories",
  "userId": "developer123"
}
```

### Manage Pull Requests
```json
POST /api/v1/devops/chat
{
  "message": "List all pull requests in luisiitodev/my-repo",
  "userId": "developer123"
}
```

## 🔧 Configuration

### API Configuration (`appsettings.json`)
```json
{
  "GitHub": {
    "Token": "your-github-token",
    "BaseUrl": "https://api.github.com"
  },
  "SemanticKernel": {
    "ApiKey": "your-openai-api-key"
  }
}
```

### Web Configuration
The web application connects to the API at `https://localhost:7062/` by default.

## 📚 Project Structure

### DevOpsAssistant.Api
- **Controllers**: API endpoints and routing
- **DTOs**: Data transfer objects for API communication
- **Handlers**: Request/response processing
- **Configuration**: OpenAPI and service configuration

### DevOpsAssistant.Application
- **Use Cases**: Business logic implementation
- **Interfaces**: Contracts for services and repositories
- **Extensions**: Dependency injection configuration

### DevOpsAssistant.Domain
- **Entities**: Domain models
- **Enums**: Domain enumerations

### DevOpsAssistant.Infrastructure
- **Services**: External service implementations
- **SemanticKernel**: AI integration
- **Configuration**: Infrastructure setup

### DevOpsAssistant.Web
- **Components**: Blazor UI components
- **Interfaces**: API service contracts
- **DTOs**: Web-specific data models

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🙏 Acknowledgments

- Built with .NET 9.0 and Blazor Server
- Powered by GitHub APIs (Octokit & GraphQL)
- Enhanced with AI capabilities through Microsoft Semantic Kernel
- API documentation with Scalar and OpenAPI

---

**Happy DevOps! 🚀**
