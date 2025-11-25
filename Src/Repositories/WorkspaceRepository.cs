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
            if (_context.Workspaces.Any(w => w.Name == createWorkspaceDto.Name))
            {
                return false;
            }

            try
            {
                var imageUrl = await _cloudinaryService.UploadImageAsync(createWorkspaceDto.Image);

                if (string.IsNullOrEmpty(imageUrl))
                {
                    throw new Exception("Image upload failed.");
                }

                _context.Workspaces.Add(createWorkspaceDto.ToWorkspace(imageUrl));
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error uploading image: " + ex.Message);
            }
            
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

        public Task<bool> UpdateWorkspace(Guid workspaceId, UpdateWorkspaceDto updateWorkspaceDto)
        {
            var workspace = _context.Workspaces.FirstOrDefault(w => w.Id == workspaceId);

            if (workspace == null)
            {
                return Task.FromResult(false);
            } else if (workspace.IsActive == false)
            {
                return Task.FromResult(false);
            } 
            else if (_context.Workspaces.Any(w => w.Name == updateWorkspaceDto.Name))
            {
                throw new Exception("A workspace with the same name already exists.");
            }
            else if (workspace.Name == updateWorkspaceDto.Name)
            {
                throw new Exception("The new name is the same as the current name.");
            } 

            workspace.Name = updateWorkspaceDto.Name ?? workspace.Name;

            try
            {
                if (updateWorkspaceDto.Image != null)
                {
                    var imageUrl = _cloudinaryService.UploadImageAsync(updateWorkspaceDto.Image).Result;

                    if (string.IsNullOrEmpty(imageUrl))
                    {
                        throw new Exception("Image upload failed.");
                    }

                    workspace.Image = imageUrl;
                }

                return Task.FromResult(true);
            } catch (Exception ex)
            {
                throw new Exception("Error uploading image: " + ex.Message);
            }
    
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
    }
}