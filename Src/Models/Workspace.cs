using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.Src.Models
{
    public class Workspace
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Theme { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public Guid OwnerId { get; set; }
        public List<WorkspaceMember> Members { get; set; } = new List<WorkspaceMember>();
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}