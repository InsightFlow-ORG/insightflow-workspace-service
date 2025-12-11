using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.Src.Configurations
{
    /// <summary>
    /// Configuración para Cloudinary
    /// </summary>
    public class CloudinarySettings
    {
        /// <summary>
        /// Nombre de la nube en Cloudinary
        /// </summary>
        public string CloudName { get; set; } = string.Empty;
        /// <summary>
        /// Clave de la API de Cloudinary
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;
        /// <summary>
        /// Secreto de la API de Cloudinary
        /// </summary>
        public string ApiSecret { get; set; } = string.Empty;
    }
}