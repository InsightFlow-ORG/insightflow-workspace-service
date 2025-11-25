using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.Src.Dtos;
using insightflow_workspace_service.Src.Models;

namespace insightflow_workspace_service.Src.Mappers
{
    public static class WorkspaceMapper
    {
        public static Workspace ToWorkspace(this CreateWorkspaceDto createWorkspaceDto)
        {
            return new Workspace
            {
                Name = createWorkspaceDto.Name,
                Description = createWorkspaceDto.Description,
                Theme = createWorkspaceDto.Theme,
                Image = createWorkspaceDto.Image,
                OwnerId = createWorkspaceDto.OwnerId,
                Members = new List<WorkspaceMember>
                {
                    new WorkspaceMember
                    {
                        Id = createWorkspaceDto.OwnerId,
                        UserName = createWorkspaceDto.Username,
                        Role = 0
                    }
                },
            };
        }

        public static WorkspaceDto ToWorkspaceDto(this Workspace workspace)
        {
            return new WorkspaceDto
            {
                Id = workspace.Id,
                Name = workspace.Name,
                Description = workspace.Description,
                Theme = workspace.Theme,
                Image = workspace.Image,
                OwnerId = workspace.OwnerId,
                workspaceMembers = workspace.Members
            };
        }
    }
}