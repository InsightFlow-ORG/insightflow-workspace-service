using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.Src.Data;
using insightflow_workspace_service.Src.Dtos;
using insightflow_workspace_service.Src.Interface;
using insightflow_workspace_service.Src.Mappers;
using insightflow_workspace_service.Src.Models;
using insightflow_workspace_service.Src.Shared;

namespace insightflow_workspace_service.Src.Repositories
{
    public class WorkspaceRepository : IWorkspaceRepository
    {
        private readonly Context _context;
        private readonly ICloudinaryService _cloudinaryService;

        public WorkspaceRepository(Context context, ICloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<Result<bool>> CreateWorkspace(CreateWorkspaceDto createWorkspaceDto)
        {
            if (_context.Workspaces.Any(w => w.Name.Equals(createWorkspaceDto.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return Result<bool>.Conflict("Workspace with the same name already exists.");
            }

            var imageUrl = await _cloudinaryService.UploadImageAsync(createWorkspaceDto.Image);

            if (string.IsNullOrEmpty(imageUrl)) return Result<bool>.BadRequest("Image upload failed.");

            _context.Workspaces.Add(createWorkspaceDto.ToWorkspace(imageUrl));
            return Result<bool>.Success(true);

        }

        public Task<Result<List<WorkspaceByUserDto>>> GetAllWorkspacesByUser(Guid userId)
        {
            var workspaces = _context.Workspaces
                .Where(w => w.Members.Any(u => u.Id == userId))
                .Where(w => w.IsActive)
                .Select(w => w.ToWorkspaceByUser(userId))
                .Where(dto => dto != null)
                .Select(dto => dto!)
                .ToList();
            
            if (workspaces.Count == 0)
            {
                return Task.FromResult(Result<List<WorkspaceByUserDto>>.NotFound("No workspaces found for the specified user."));
            }
            
            return Task.FromResult(Result<List<WorkspaceByUserDto>>.Success(workspaces));
        }

        public Task<Result<WorkspaceDto?>> GetWorkspaceById(Guid WorkspaceId)
        {
            var workspace = _context.Workspaces.FirstOrDefault(w => w.Id == WorkspaceId);

            if (workspace == null)
            {
                return Task.FromResult(Result<WorkspaceDto?>.NotFound("Workspace not found."));
            } if (workspace.IsActive == false)
            {
                return Task.FromResult(Result<WorkspaceDto?>.NotFound("Workspace not found."));
            }

            return Task.FromResult(Result<WorkspaceDto?>.Success(workspace.ToWorkspaceDto()));
        }

        public async Task<Result<bool>> UpdateWorkspace(Guid workspaceId, UpdateWorkspaceDto updateWorkspaceDto)
        {
            var workspace = _context.Workspaces.FirstOrDefault(w => w.Id == workspaceId);

            if (workspace == null) return Result<bool>.NotFound("Workspace not found.");
            if (!workspace.IsActive) return Result<bool>.BadRequest("Workspace is not active.");
            if (_context.Workspaces.Any(w => w.Name == updateWorkspaceDto.Name && w.Id != workspaceId)) return Result<bool>.Conflict("Workspace with the same name already exists.");
            
            workspace.Name = updateWorkspaceDto.Name ?? workspace.Name;
            workspace.Description = updateWorkspaceDto.Description ??  workspace.Description;
            workspace.Theme = updateWorkspaceDto.Theme ?? workspace.Theme;

            if (updateWorkspaceDto.Image != null)
            {
                var imageUrl = await _cloudinaryService.UploadImageAsync(updateWorkspaceDto.Image);

                if (string.IsNullOrEmpty(imageUrl))
                {
                    return Result<bool>.BadRequest("Image upload failed.");
                }

                workspace.Image = imageUrl;
            }

            return Result<bool>.Success(true);
    
        }

        public Task<Result<bool>> DeleteWorkspace(Guid workspaceId)
        {
            var workspace = _context.Workspaces.FirstOrDefault(w => w.Id == workspaceId);

            if (workspace == null)
            {
                return Task.FromResult(Result<bool>.NotFound("Workspace not found."));
            } else if (workspace.IsActive == false)
            {
                return Task.FromResult(Result<bool>.BadRequest("Workspace is not active."));
            }

            workspace.IsActive = false;
            return Task.FromResult(Result<bool>.Success(true));
        }

        public Task<Result<List<Workspace>>> GetAllWorkspaces()
        {
            var workspaces = _context.Workspaces.ToList();
            return Task.FromResult(Result<List<Workspace>>.Success(workspaces));
        }
    }
}