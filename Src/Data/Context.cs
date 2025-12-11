using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.Src.Models;

namespace insightflow_workspace_service.Src.Data
{
    /// <summary>
    /// Simulación de un contexto de base de datos en memoria.
    /// </summary>
    public class Context
    {
        /// <summary>
        /// Lista de espacios de trabajo en memoria.
        /// </summary>
        public List<Workspace> Workspaces { get; set; } = new List<Workspace>();
    }
}