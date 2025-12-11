using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.Src.Models
{
    /// <summary>
    /// Representa un workspace en el sistema.
    /// </summary>
    public class Workspace
    {
        /// <summary>
        /// Identificador único del workspace.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Nombre del workspace.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripción del workspace.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Tema visual del workspace.
        /// </summary>
        public string Theme { get; set; } = string.Empty;

        /// <summary>
        /// Imagen representativa del workspace.
        /// </summary>
        public string Image { get; set; } = string.Empty;

        /// <summary>
        /// Identificador del propietario del workspace.
        /// </summary>
        public Guid OwnerId { get; set; }

        /// <summary>
        /// Lista de miembros del workspace.
        /// </summary>
        public List<WorkspaceMember> Members { get; set; } = new List<WorkspaceMember>();

        /// <summary>
        /// Indica si el workspace está activo.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Fecha de creación del workspace.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Fecha de la última actualización del workspace.
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}