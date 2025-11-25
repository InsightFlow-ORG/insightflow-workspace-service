using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.Src.Models
{
    public class WorkspaceMember
    {
        public Guid Id { get; set; }
        
        public string UserName { get; set; } = string.Empty;

        [RegularExpression("^(Owner|Editor)$", ErrorMessage = "Role must be either 'Owner' or 'Editor'.")]
        public string Role { get; set; } = string.Empty;

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}