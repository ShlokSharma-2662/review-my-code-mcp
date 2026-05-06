# MCP Demo App

This folder contains a deliberately bad C# file for demonstrating how your MCP code-review server works.

## Demo File

- `RuleBreakerDemo.cs`

It is intentionally designed to trigger every implemented rule category:

- async correctness
- security
- performance
- maintainability
- method
- type design
- file-folder
- c# 10 usage

## How to Demo in MCP Client

1. Start the MCP server from this repository.
2. Call tool `review_csharp_code`.
3. Pass the full text of `app/RuleBreakerDemo.cs` as the `code` argument.
4. Use `maxIssues: 50` so all findings can be returned.

## What To Show In Demo Output

The response now includes:

- `invocationId`: unique call id (use to confirm usage in logs)
- `totalRulesChecked`: total executed rules
- `totalRulesMatched`: total matched rules
- `checkedCategories`: categories that were evaluated
- `categoryScores`: score per category with counts
- `issues`: returned issues (bounded by `maxIssues`)
- `suggestedChanges`: normalized fix list for UI/reporting

## Suggested Demo Narrative

1. "We call `review_csharp_code` with the sample file."
2. "The server checks all categories and reports exactly what was checked."
3. "You can see what matched, suggested fixes, and per-category score."
4. "`invocationId` in response maps to server log entry, proving tool usage."
