using System;
using DotNetEnv;
using insightflow_workspace_service.Src.Configurations;
using insightflow_workspace_service.Src.Data;
using insightflow_workspace_service.Src.Interface;
using insightflow_workspace_service.Src.Repositories;
using insightflow_workspace_service.Src.Service;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddSingleton<Context>();
builder.Services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();

var claudinarySettings = new CloudinarySettings()
{
    ApiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY")!,
    ApiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET")!,
    CloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME")!
};

builder.Services.Configure<CloudinarySettings>(options =>
{
    options.ApiKey = claudinarySettings.ApiKey;
    options.ApiSecret = claudinarySettings.ApiSecret;
    options.CloudName = claudinarySettings.CloudName;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextJS", policy =>
    {
        policy.WithOrigins(
            "http://localhost:3000",
            "https://insightflow-frontend-fd042.web.app",
            "https://insightflow-frontend-fd042.firebaseapp.com"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Context>();
    Seeder.Seed(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowNextJS");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();