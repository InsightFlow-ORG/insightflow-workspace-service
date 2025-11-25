using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.Src.Dtos;
using insightflow_workspace_service.Src.Models;

namespace insightflow_workspace_service.Src.Interface
{
    public interface IWorkspaceRepository
    {
        public Task<bool> CreateWorkspace(CreateWorkspaceDto createWorkspaceDto);
        public Task<List<WorkspaceByUserDto>> GetAllWorkspacesByUser(Guid UserId);
        public Task<WorkspaceDto?> GetWorkspaceById(Guid WorkspaceId);
        public Task<bool> UpdateWorkspace(Guid workspaceId, UpdateWorkspaceDto updateWorkspaceDto);
        public Task<bool> DeleteWorkspace(Guid workspaceId);
        public Task<List<Workspace>> GetAllWorkspaces();
    }
}