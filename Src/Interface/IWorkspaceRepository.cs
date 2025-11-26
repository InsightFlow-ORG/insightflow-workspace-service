using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.Src.Dtos;
using insightflow_workspace_service.Src.Models;
using insightflow_workspace_service.Src.Shared;

namespace insightflow_workspace_service.Src.Interface
{
    public interface IWorkspaceRepository
    {
        public Task<Result<bool>> CreateWorkspace(CreateWorkspaceDto createWorkspaceDto);
        public Task<Result<List<WorkspaceByUserDto>>> GetAllWorkspacesByUser(Guid UserId);
        public Task<Result<WorkspaceDto?>> GetWorkspaceById(Guid WorkspaceId);
        public Task<Result<bool>> UpdateWorkspace(Guid workspaceId, UpdateWorkspaceDto updateWorkspaceDto);
        public Task<Result<bool>> DeleteWorkspace(Guid workspaceId);
        public Task<Result<List<Workspace>>> GetAllWorkspaces();
    }
}