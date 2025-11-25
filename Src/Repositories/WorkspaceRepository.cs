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

        public WorkspaceRepository(Context context)
        {
            _context = context;
        }

        public Task<bool> CreateWorkspace(CreateWorkspaceDto createWorkspaceDto)
        {
            if (_context.Workspaces.Any(w => w.Name == createWorkspaceDto.Name))
            {
                return Task.FromResult(false);
            }

            _context.Workspaces.Add(createWorkspaceDto.ToWorkspace());
            return Task.FromResult(true);
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
            } else if (workspace.Name == updateWorkspaceDto.Name)
            {
                throw new Exception("The new name is the same as the current name.");
            } 

            workspace.Name = updateWorkspaceDto.Name ?? workspace.Name;
            workspace.Image = updateWorkspaceDto.Image ?? workspace.Image;

            return Task.FromResult(true);
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