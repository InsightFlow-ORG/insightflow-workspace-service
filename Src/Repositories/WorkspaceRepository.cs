using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.Src.Data;
using insightflow_workspace_service.Src.Dtos;
using insightflow_workspace_service.Src.Interface;
using insightflow_workspace_service.Src.Mappers;
using insightflow_workspace_service.Src.Models;

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

        public async Task<bool> CreateWorkspace(CreateWorkspaceDto createWorkspaceDto)
        {
            if (_context.Workspaces.Any(w => w.Name.Equals(createWorkspaceDto.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            var imageUrl = await _cloudinaryService.UploadImageAsync(createWorkspaceDto.Image);

            if (string.IsNullOrEmpty(imageUrl)) return false;

            _context.Workspaces.Add(createWorkspaceDto.ToWorkspace(imageUrl));
            return true;

        }

        public Task<List<WorkspaceByUserDto>> GetAllWorkspacesByUser(Guid userId)
        {
            var workspaces = _context.Workspaces
                .Where(w => w.Members.Any(u => u.Id == userId))
                .Where(w => w.IsActive)
                .Select(w => w.ToWorkspaceByUser(userId))
                .Where(dto => dto != null)
                .Select(dto => dto!)
                .ToList();
            
            return Task.FromResult(workspaces);
        }

        public Task<WorkspaceDto?> GetWorkspaceById(Guid WorkspaceId)
        {
            var workspace = _context.Workspaces.FirstOrDefault(w => w.Id == WorkspaceId);

            if (workspace == null)
            {
                return Task.FromResult<WorkspaceDto?>(null);
            } if (workspace.IsActive == false)
            {
                return Task.FromResult<WorkspaceDto?>(null);
            }

            return Task.FromResult<WorkspaceDto?>(workspace.ToWorkspaceDto());
        }

        public async Task<bool> UpdateWorkspace(Guid workspaceId, UpdateWorkspaceDto updateWorkspaceDto)
        {
            var workspace = _context.Workspaces.FirstOrDefault(w => w.Id == workspaceId);

            if (workspace == null) return false;
            if (!workspace.IsActive) return false;
            if (_context.Workspaces.Any(w => w.Name == updateWorkspaceDto.Name && w.Id != workspaceId)) return false;
            if (workspace.Name == updateWorkspaceDto.Name) return false;
            
            workspace.Name = updateWorkspaceDto.Name ?? workspace.Name;
            workspace.Description = updateWorkspaceDto.Description ??  workspace.Description;
            workspace.Theme = updateWorkspaceDto.Theme ?? workspace.Theme;

            if (updateWorkspaceDto.Image != null)
            {
                var imageUrl = await _cloudinaryService.UploadImageAsync(updateWorkspaceDto.Image);

                if (string.IsNullOrEmpty(imageUrl))
                {
                    return false;
                }

                workspace.Image = imageUrl;
            }

            return true;
    
        }

        public Task<bool> DeleteWorkspace(Guid workspaceId)
        {
            var workspace = _context.Workspaces.FirstOrDefault(w => w.Id == workspaceId);

            if (workspace == null)
            {
                return Task.FromResult(false);
            } else if (workspace.IsActive == false)
            {
                return Task.FromResult(false);
            }

            workspace.IsActive = false;
            return Task.FromResult(true);
        }

        public Task<List<Workspace>> GetAllWorkspaces()
        {
            var workspaces = _context.Workspaces.ToList();
            return Task.FromResult(workspaces);
        }
    }
}