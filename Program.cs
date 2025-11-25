using insightflow_workspace_service.Src.Data;
using insightflow_workspace_service.Src.Interface;
using insightflow_workspace_service.Src.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddSingleton<Context>();
builder.Services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();