# MCP C# Code Review Server

## What This Project Is

This project is a production-ready MCP server built in C# and .NET. It provides automated code review tools focused on C# quality, security, async correctness, performance, maintainability, naming conventions, and project organization signals.

The server communicates over stdio transport, which makes it easy to plug into MCP-compatible AI clients.

## Tech Stack

[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)
[![.NET 8](https://img.shields.io/badge/.NET%208-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![MCP](https://img.shields.io/badge/Model%20Context%20Protocol-111111?style=for-the-badge&logo=protocolsdotio&logoColor=white)](https://modelcontextprotocol.io/)
[![JSON](https://img.shields.io/badge/JSON-000000?style=for-the-badge&logo=json&logoColor=white)](https://www.json.org/)
[![Markdown](https://img.shields.io/badge/Markdown-000000?style=for-the-badge&logo=markdown&logoColor=white)](https://www.markdownguide.org/)
[![GitHub](https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white)](https://github.com/)

## AI Client Compatibility

This server can be connected to any MCP-compatible AI client app, including:

- Cursor
- Visual Studio Code MCP-capable clients
- Claude Code
- GitHub Copilot environments that support MCP
- Other MCP-compatible tools

If a client supports custom MCP server definitions (command + args), it can connect to this server.

## Main MCP Tools

### review_csharp_code

Reviews C# source input and returns structured JSON with:
- summary
- score
- issues list (severity, category, line, description, fix)
- invocationId (unique id per review request)
- totalRulesChecked / totalRulesMatched
- checkedCategories
- categoryScores (score + coverage per category)
- suggestedChanges (normalized list for easy rendering)

### health_check

Returns server health and timestamp.

### get_rule_backlog

Reads the Add New Rules sections from category markdown files and returns pending proposals as JSON.


## MCP Connection JSON

Use one of the following entries in your MCP client configuration.

### Option A: Run from csproj

```json
{
  "mcpServers": {
    "csharp-code-review": {
      "command": "dotnet",
      "args": [
        "run",
        "--project",
        "/absolute/path/to/McpCodeReviewServer.csproj"
      ]
    }
  }
}
```

### Option B: Run from built dll

```json
{
  "mcpServers": {
    "csharp-code-review": {
      "command": "dotnet",
      "args": [
        "/absolute/path/to/bin/Debug/net8.0/McpCodeReviewServer.dll"
      ]
    }
  }
}
```

For production use, prefer a Release build path:
- /absolute/path/to/bin/Release/net8.0/McpCodeReviewServer.dll

## Project Architecture

The project is organized by separation of concerns:

- Tools
  - MCP tool endpoints only
- Services
  - analysis engine, scoring, rule backlog reader, DI registration
- Rules
  - category-based providers and reusable rule abstractions
- Models
  - response contracts
- documentation/rule-catalog
  - rule documentation per category

## Current Rule Categories

- Async
- Security
- Performance
- Maintainability
- Method
- Type Design (class/interface/record)
- File and Folder
- CSharp Modernization

## Rule Documentation Workflow

Each rule category has a dedicated markdown file under documentation/rule-catalog.

Each category file has exactly two sections:

1. Existing Rules
- Rules already implemented in code.

2. Add New Rules
- Backlog entries for future implementation.
- The get_rule_backlog tool reads these entries.

You can extend the project by adding your own entries in the Add New Rules section of each category markdown file. This lets teams propose new checks without changing code first.

## How to Run Locally

### Prerequisites

- .NET SDK installed
- .NET 8 runtime (project targets net8.0)

### Build

dotnet build

### Run

dotnet run --project McpCodeReviewServer.csproj

## How to Connect an AI Client

1. Open your AI client MCP configuration.
2. Add one server entry using either the csproj or dll JSON from the top of this README.
3. Restart the client or reload MCP servers.
4. Verify the server is connected and tools are visible.
5. Call review_csharp_code with C# source content.

## Example review_csharp_code input shape

Send raw C# code as the code argument and optional maxIssues integer.

Example intent:
- code: full C# source
- maxIssues: 50

## Development Notes

- Rules are implemented as pluggable providers.
- Add new executable checks by creating or extending a provider in Rules.
- Register provider in Services/ServiceCollectionExtensions.cs.
- Keep docs in sync by updating the corresponding documentation/rule-catalog category file.

## Extending Rules via Markdown

To extend this project, open the category file in documentation/rule-catalog and add a new bullet under Add New Rules.

Suggested entry format:
- Rule name: <short rule title>; Category: <category>; Severity: <critical|warning|suggestion>; Detection: <pattern/condition>; Fix: <recommended action>

After adding entries, call get_rule_backlog to verify your new items are discoverable by MCP clients.

## Production Guidance

- Use Release build output for client integration.
- Pin package versions in CI.
- Add automated tests for each rule provider.
- Validate new rules against false positive and false negative scenarios.

## Author

Shlok Sharma is the author of this MCP C# Code Review Server, focused on building practical developer tooling for automated code quality, security, and maintainability checks.

## Quick File Map

- Program.cs
- Tools/CodeReviewTool.cs
- Services/ReviewAnalyzer.cs
- Services/ReviewScorer.cs
- Services/MarkdownRuleBacklogService.cs
- Rules/Abstractions/*
- Rules/<Category>/*
- documentation/rules.md
- documentation/rule-catalog/*.md

## Cursor MCP Review Demo Screenshots

### 1. Starting MCP Review Workflow in Cursor
This view shows Cursor starting the MCP workflow for app review, verifying tool schemas, and running the health check to confirm the server is active.

![Starting MCP review workflow in Cursor](app/screenshots/Running%20MCP%20server.png)

### 2. Tool Response with Rule Coverage and Scoring Metadata
This output shows the `review_csharp_code` response structure including `invocationId`, `totalRulesChecked`, `totalRulesMatched`, `checkedCategories`, and `categoryScores`.

![MCP review response with coverage and category scores](app/screenshots/Showing%20file%20findings.png)

### 3. Prioritized Findings from Reviewing app/RuleBreakerDemo.cs
This screen highlights prioritized review findings from the demo app, including critical and warning security/async issues and a summarized overall result.

![Prioritized findings from app review](app/screenshots/Showing%20file%20findings%202.png)

### 4. Review Wrap-Up: Assumptions and Change Summary
This final screen shows the post-review summary, assumptions, and what actions were or were not applied to code after running the MCP review workflow.

![Review wrap-up with assumptions and change summary](app/screenshots/Summary.png)
