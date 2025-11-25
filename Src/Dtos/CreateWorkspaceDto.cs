using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.Src.Dtos
{
    public class CreateWorkspaceDto
    {
        [Required(ErrorMessage = "Workspace name is required.")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Workspace description is required.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Workspace theme is required.")]
        public string Theme { get; set; } = string.Empty;

        [Required(ErrorMessage = "Workspace image is required.")]
        public IFormFile Image { get; set; } = null!;

        [Required(ErrorMessage = "Workspace owner ID is required.")]
        public Guid OwnerId { get; set; }

        [Required(ErrorMessage = "Workspace username is required.")]
        public string Username { get; set; } = string.Empty;
    }
}