using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.Src.Models
{
    /// <summary>
    /// Representa un miembro de un workspace.
    /// </summary>
    public class WorkspaceMember
    {
        /// <summary>
        /// Identificador único del miembro del workspace.
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Nombre de usuario del miembro del workspace.
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Rol del miembro en el workspace.
        /// </summary>
        [RegularExpression("^(Owner|Editor)$", ErrorMessage = "Role must be either 'Owner' or 'Editor'.")]
        public string Role { get; set; } = string.Empty;

        /// <summary>
        /// Fecha en que el miembro se unió al workspace.
        /// </summary>
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}