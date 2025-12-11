using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.Src.Dtos
{
    /// <summary>
    /// Dto para la creación de un nuevo espacio de trabajo.
    /// </summary>
    public class CreateWorkspaceDto
    {
        /// <summary>
        /// Nombre del espacio de trabajo.
        /// </summary>
        [Required(ErrorMessage = "Workspace name is required.")]
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Descripción del espacio de trabajo.
        /// </summary>
        [Required(ErrorMessage = "Workspace description is required.")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Tema del espacio de trabajo.
        /// </summary>
        [Required(ErrorMessage = "Workspace theme is required.")]
        public string Theme { get; set; } = string.Empty;

        /// <summary>
        /// Imagen del espacio de trabajo.
        /// </summary>
        [Required(ErrorMessage = "Workspace image is required.")]
        public IFormFile Image { get; set; } = null!;

        /// <summary>
        /// ID del propietario del espacio de trabajo.
        /// </summary>
        [Required(ErrorMessage = "Workspace owner ID is required.")]
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Nombre de usuario del espacio de trabajo.
        /// </summary>
        [Required(ErrorMessage = "Workspace username is required.")]
        public string Username { get; set; } = string.Empty;
    }
}