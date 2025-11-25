using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.Src.Dtos
{
    public class UpdateWorkspaceDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Theme { get; set; }
        public IFormFile? Image { get; set; }

    }
}