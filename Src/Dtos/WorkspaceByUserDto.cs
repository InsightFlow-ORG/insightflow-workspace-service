using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.Src.Models;

namespace insightflow_workspace_service.Src.Dtos
{
    /// <summary>
    /// Dto que representa un espacio de trabajo asociado a un usuario.
    /// </summary>
    public class WorkspaceByUserDto
    {
        /// <summary>
        /// Identificador único del espacio de trabajo.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre del espacio de trabajo.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Imagen del espacio de trabajo.
        /// </summary>
        public string Image { get; set; } = string.Empty;

        /// <summary>
        /// Rol del usuario en el espacio de trabajo.
        /// </summary>
        public string Role { get; set; } = string.Empty;  
    }
}