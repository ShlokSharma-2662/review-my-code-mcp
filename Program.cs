using McpCodeReviewServer.Tools;
using McpCodeReviewServer.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddCodeReviewEngine();

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<CodeReviewTool>();

await builder.Build().RunAsync();
