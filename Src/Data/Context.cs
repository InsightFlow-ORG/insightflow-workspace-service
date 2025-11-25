using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.Src.Models;

namespace insightflow_workspace_service.Src.Data
{
    public class Context
    {
        public List<Workspace> Workspaces { get; set; } = new List<Workspace>();
    }
}