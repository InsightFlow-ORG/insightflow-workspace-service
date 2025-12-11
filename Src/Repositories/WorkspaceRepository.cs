using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.Src.Data;
using insightflow_workspace_service.Src.Dtos;
using insightflow_workspace_service.Src.Interface;
using insightflow_workspace_service.Src.Mappers;
using insightflow_workspace_service.Src.Models;
using insightflow_workspace_service.Src.Shared;

namespace insightflow_workspace_service.Src.Repositories
{
    /// <summary>
    /// Clase que implementa el repositorio de espacios de trabajo.
    /// </summary>
    public class WorkspaceRepository : IWorkspaceRepository
    {
        /// <summary>
        /// Contexto de la base de datos.
        /// </summary>
        private readonly Context _context;

        /// <summary>
        /// Servicio de Cloudinary para la gestión de imágenes.
        /// </summary>
        private readonly ICloudinaryService _cloudinaryService;

        /// <summary>
        /// Constructor de la clase WorkspaceRepository.
        /// </summary>
        /// <param name="context">Contexto de la base de datos.</param>
        /// <param name="cloudinaryService">Servicio de Cloudinary para la gestión de imágenes.</param>
        public WorkspaceRepository(Context context, ICloudinaryService cloudinaryService)
        {
            _context = context;
            _cloudinaryService = cloudinaryService;
        }

        /// <summary>
        /// Crea un nuevo espacio de trabajo.
        /// </summary>
        /// <param name="createWorkspaceDto">Datos para crear un nuevo espacio de trabajo.</param>
        /// <returns>Resultado de la operación indicando éxito o error.</returns>
        public async Task<Result<bool>> CreateWorkspace(CreateWorkspaceDto createWorkspaceDto)
        {
            if (_context.Workspaces.Any(w => w.Name.Equals(createWorkspaceDto.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return Result<bool>.Conflict("Workspace with the same name already exists.");
            }

            var imageUrl = await _cloudinaryService.UploadImageAsync(createWorkspaceDto.Image);

            if (string.IsNullOrEmpty(imageUrl)) return Result<bool>.BadRequest("Image upload failed.");

            _context.Workspaces.Add(createWorkspaceDto.ToWorkspace(imageUrl));
            return Result<bool>.Success(true);

        }

        /// <summary>
        /// Obtiene todos los espacios de trabajo asociados a un usuario.
        /// </summary>
        /// <param name="userId">Identificador del usuario.</param>
        /// <returns>Lista de espacios de trabajo asociados al usuario.</returns>
        public Task<Result<List<WorkspaceByUserDto>>> GetAllWorkspacesByUser(Guid userId)
        {
            var workspaces = _context.Workspaces
                .Where(w => w.Members.Any(u => u.Id == userId))
                .Where(w => w.IsActive)
                .Select(w => w.ToWorkspaceByUser(userId))
                .Where(dto => dto != null)
                .Select(dto => dto!)
                .ToList();
            
            if (workspaces.Count == 0)
            {
                return Task.FromResult(Result<List<WorkspaceByUserDto>>.NotFound("No workspaces found for the specified user."));
            }
            
            return Task.FromResult(Result<List<WorkspaceByUserDto>>.Success(workspaces));
        }

        /// <summary>
        /// Obtiene un espacio de trabajo por su ID.
        /// </summary>
        /// <param name="WorkspaceId">Identificador del espacio de trabajo.</param>
        /// <returns>Espacio de trabajo correspondiente al ID proporcionado.</returns>
        public Task<Result<WorkspaceDto?>> GetWorkspaceById(Guid WorkspaceId)
        {
            var workspace = _context.Workspaces.FirstOrDefault(w => w.Id == WorkspaceId);

            if (workspace == null)
            {
                return Task.FromResult(Result<WorkspaceDto?>.NotFound("Workspace not found."));
            } if (workspace.IsActive == false)
            {
                return Task.FromResult(Result<WorkspaceDto?>.NotFound("Workspace not found."));
            }

            return Task.FromResult(Result<WorkspaceDto?>.Success(workspace.ToWorkspaceDto()));
        }
        
        /// <summary>
        /// Actualiza un espacio de trabajo existente.
        /// </summary>
        /// <param name="workspaceId">Identificador del espacio de trabajo.</param>
        /// <param name="updateWorkspaceDto">Datos para actualizar el espacio de trabajo.</param>
        /// <returns>Resultado de la operación indicando éxito o error.</returns>
        public async Task<Result<bool>> UpdateWorkspace(Guid workspaceId, UpdateWorkspaceDto updateWorkspaceDto)
        {
            var workspace = _context.Workspaces.FirstOrDefault(w => w.Id == workspaceId);

            if (workspace == null) return Result<bool>.NotFound("Workspace not found.");
            if (!workspace.IsActive) return Result<bool>.BadRequest("Workspace is not active.");
            if (_context.Workspaces.Any(w => w.Name == updateWorkspaceDto.Name && w.Id != workspaceId)) return Result<bool>.Conflict("Workspace with the same name already exists.");
            
            workspace.Name = updateWorkspaceDto.Name ?? workspace.Name;
            workspace.Description = updateWorkspaceDto.Description ??  workspace.Description;
            workspace.Theme = updateWorkspaceDto.Theme ?? workspace.Theme;

            if (updateWorkspaceDto.Image != null)
            {
                var imageUrl = await _cloudinaryService.UploadImageAsync(updateWorkspaceDto.Image);

                if (string.IsNullOrEmpty(imageUrl))
                {
                    return Result<bool>.BadRequest("Image upload failed.");
                }

                workspace.Image = imageUrl;
            }

            return Result<bool>.Success(true);
    
        }  

        /// <summary>
        /// Elimina (desactiva) un espacio de trabajo por su ID.
        /// </summary>
        /// <param name="workspaceId">Identificador del espacio de trabajo.</param>
        /// <returns>Resultado de la operación indicando éxito o error.</returns>
        public Task<Result<bool>> DeleteWorkspace(Guid workspaceId)
        {
            var workspace = _context.Workspaces.FirstOrDefault(w => w.Id == workspaceId);

            if (workspace == null)
            {
                return Task.FromResult(Result<bool>.NotFound("Workspace not found."));
            } else if (workspace.IsActive == false)
            {
                return Task.FromResult(Result<bool>.BadRequest("Workspace is not active."));
            }

            workspace.IsActive = false;
            return Task.FromResult(Result<bool>.Success(true));
        }

        /// <summary>
        /// Obtiene todos los espacios de trabajo.
        /// </summary>
        /// <returns>Lista de todos los espacios de trabajo.</returns>
        public Task<Result<List<Workspace>>> GetAllWorkspaces()
        {
            var workspaces = _context.Workspaces.ToList();
            return Task.FromResult(Result<List<Workspace>>.Success(workspaces));
        }
    }
}