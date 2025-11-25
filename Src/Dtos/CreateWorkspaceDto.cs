using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.Src.Dtos
{
    public class CreateWorkspaceDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Theme { get; set; } = string.Empty;
        [Required]
        public IFormFile Image { get; set; } = null!;
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
    }
}