using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.Src.Models;

namespace insightflow_workspace_service.Src.Dtos
{
    public class WorkspaceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Theme { get; set; } = string.Empty;
        public IFormFile Image { get; set; } = null!;
        public Guid OwnerId { get; set; }  
        public List<WorkspaceMember> workspaceMembers { get; set; } = new List<WorkspaceMember>();
    }
}