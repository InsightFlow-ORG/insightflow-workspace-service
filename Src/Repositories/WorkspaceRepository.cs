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

            if (userId == Guid.Empty)
            {
                return Task.FromResult(new List<WorkspaceByUserDto>());
            }

            var workspaces = _context.Workspaces
                .Where(w => w.Members.Any(u => u.Id == userId))
                .Select(w => w.ToWorkspaceByUser(userId))
                .Where(dto => dto != null)
                .Select(dto => dto!)
                .ToList();
            
            return Task.FromResult(workspaces);
        }
    }
}