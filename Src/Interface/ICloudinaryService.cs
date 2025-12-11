using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace insightflow_workspace_service.Src.Interface
{
    /// <summary>
    /// Interface para el servicio de Cloudinary.
    /// </summary>
    public interface ICloudinaryService
    {
        /// <summary>
        /// Sube una imagen a Cloudinary y devuelve la URL de la imagen subida.
        /// </summary>
        /// <param name="file">Archivo de imagen a subir.</param>
        /// <returns>URL de la imagen subida.</returns>
        Task<string> UploadImageAsync(IFormFile file);
    }
}