using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.Src.Dtos;
using insightflow_workspace_service.Src.Models;

namespace insightflow_workspace_service.Src.Mappers
{
    /// <summary>
    /// Clase estática para mapear entre diferentes representaciones de espacios de trabajo.
    /// </summary>
    public static class WorkspaceMapper
    {
        /// <summary>
        /// Convierte un CreateWorkspaceDto en un objeto Workspace.
        /// </summary>
        /// <param name="createWorkspaceDto">Datos para crear el workspace.</param>
        /// <param name="imageUrl">URL de la imagen del workspace.</param>
        /// <returns>Objeto Workspace creado.</returns>
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

        /// <summary>
        /// Convierte un objeto Workspace en un WorkspaceDto.
        /// </summary>
        /// <param name="workspace">Objeto Workspace a convertir.</param>
        /// <returns>Objeto WorkspaceDto resultante.</returns>
        public static WorkspaceDto ToWorkspaceDto(this Workspace workspace)
        {
            return new WorkspaceDto
            {
                Name = workspace.Name,
                Image = workspace.Image,
                workspaceMembers = workspace.Members
            };
        }

        /// <summary>
        /// Convierte un objeto Workspace en un WorkspaceByUserDto basado en el ID del usuario.
        /// </summary>
        /// <param name="workspace">Objeto Workspace a convertir.</param>
        /// <param name="id">ID del usuario para filtrar el workspace.</param>
        /// <returns>Objeto WorkspaceByUserDto resultante o null si el usuario no es miembro.</returns>
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