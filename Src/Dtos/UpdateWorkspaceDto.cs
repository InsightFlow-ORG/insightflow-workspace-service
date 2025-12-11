using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.Src.Dtos
{
    /// <summary>
    /// Dto para la actualización de un espacio de trabajo.
    /// </summary>
    public class UpdateWorkspaceDto
    {
        /// <summary>
        /// Nombre del espacio de trabajo.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Descripción del espacio de trabajo.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Tema del espacio de trabajo.
        /// </summary>
        public string? Theme { get; set; }

        /// <summary>
        /// Imagen del espacio de trabajo.
        /// </summary>
        public IFormFile? Image { get; set; }

    }
}