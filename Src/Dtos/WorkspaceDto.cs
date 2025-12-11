using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.Src.Models;

namespace insightflow_workspace_service.Src.Dtos
{
    /// <summary>
    /// Dto que representa un espacio de trabajo.
    /// </summary>
    public class WorkspaceDto
    {
        /// <summary>
        /// Identificador único del espacio de trabajo.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Imagen del espacio de trabajo.
        /// </summary>
        public string Image { get; set; } = string.Empty;

        /// <summary>
        /// Miembros del espacio de trabajo.
        /// </summary>
        public List<WorkspaceMember> workspaceMembers { get; set; } = new List<WorkspaceMember>();
    }
}