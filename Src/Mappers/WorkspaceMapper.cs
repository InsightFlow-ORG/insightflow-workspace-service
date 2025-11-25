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
        public static Workspace ToWorkspace(this CreateWorkspaceDto createWorkspaceDto, string imageUrl)
        {
            return new Workspace
            {
                Name = createWorkspaceDto.Name,
                Description = createWorkspaceDto.Description,
                Theme = createWorkspaceDto.Theme,
                Image = imageUrl,
                OwnerId = createWorkspaceDto.OwnerId,
                Members = new List<WorkspaceMember>
                {
                    new WorkspaceMember
                    {
                        Id = createWorkspaceDto.OwnerId,
                        UserName = createWorkspaceDto.Username,
                        Role = "Owner"
                    }
                },
            };
        }

        public static WorkspaceDto ToWorkspaceDto(this Workspace workspace)
        {
            return new WorkspaceDto
            {
                Name = workspace.Name,
                Image = workspace.Image,
                workspaceMembers = workspace.Members
            };
        }

        public static WorkspaceByUserDto? ToWorkspaceByUser(this Workspace workspace, Guid id)
        {
            var member = workspace.Members.FirstOrDefault(u => u.Id == id);

            if (member == null)
            {
                return null;
            }

            return new WorkspaceByUserDto
            {
                Id = workspace.Id,
                Name = workspace.Name,
                Image = workspace.Image,
                Role = member.Role
            };
        }
    }
}