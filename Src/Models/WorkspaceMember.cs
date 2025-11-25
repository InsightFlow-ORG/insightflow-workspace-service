using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.Src.Models
{
    public class WorkspaceMember
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public WorkspaceRole Role { get; set; }
    }
}