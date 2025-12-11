using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using insightflow_workspace_service.Src.Configurations;
using insightflow_workspace_service.Src.Interface;
using Microsoft.Extensions.Options;

namespace insightflow_workspace_service.Src.Service
{
    /// <summary>
    /// Servicio para manejar la subida de imágenes a Cloudinary.
    /// </summary>
    public class CloudinaryService : ICloudinaryService
    {
        /// <summary>
        /// Instancia de Cloudinary para operaciones de subida.
        /// </summary>
        private readonly Cloudinary _cloudinary;

        /// <summary>
        /// Constructor que inicializa la configuración de Cloudinary.
        /// </summary>
        /// <param name="config">Configuración de Cloudinary.</param>
        public CloudinaryService(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(account);
        }

        /// <summary>
        /// Sube una imagen a Cloudinary y retorna la URL segura (HTTPS).
        /// </summary>
        /// <param name="file">Archivo de imagen a subir.</param>
        /// <returns>URL segura de la imagen subida.</returns>
        /// <exception cref="ArgumentException">Se lanza si el archivo no es válido.</exception>
        /// <exception cref="Exception">Se lanza si ocurre un error durante la subida.</exception>
        public async Task<string> UploadImageAsync(IFormFile file)
        {
            // Validar que el archivo sea una imagen
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("El archivo no puede estar vacío");
            }

            var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif" };
            if (!Array.Exists(allowedTypes, t => t == file.ContentType.ToLower()))
            {
                throw new ArgumentException("Solo se permiten imágenes (JPG, PNG, GIF)");
            }

            // Validar tamaño (máximo 5MB)
            if (file.Length > 5 * 1024 * 1024)
            {
                throw new ArgumentException("La imagen no puede superar 5MB");
            }

            // Generar nombre único para la imagen
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileNameWithoutExtension(file.FileName)}";

            using var stream = file.OpenReadStream();
            
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, stream),
                Folder = "insightflow/workspaces", // Organiza las imágenes en carpetas
                UseFilename = true,
                UniqueFilename = true,
                Overwrite = false,
                Transformation = new Transformation()
                    .Width(500)
                    .Height(500)
                    .Crop("limit") // Redimensiona si es más grande, mantiene aspecto
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
            {
                throw new Exception($"Error al subir imagen: {uploadResult.Error.Message}");
            }

            // Retorna la URL segura (HTTPS)
            return uploadResult.SecureUrl.ToString();
        }
    }
}