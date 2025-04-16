var builder = DistributedApplication.CreateBuilder(args);

#pragma warning disable ASPIREPUBLISHERS001
builder.AddAzurePublisher("azure-app");
#pragma warning restore ASPIREPUBLISHERS001

var api = builder.AddProject<Projects.ServCraftCodeSample_Api>("api");

builder.AddNpmApp("web", "../servcraftcodesample.web", "dev")
    .WithReference(api)
    .WaitFor(api)
    .WithEnvironment("BROWSER", "none") // Disable opening browser on npm start
    .WithHttpEndpoint(env: "VITE_PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

builder.Build().Run();
