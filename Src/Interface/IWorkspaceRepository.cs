using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.Src.Dtos;
using insightflow_workspace_service.Src.Models;
using insightflow_workspace_service.Src.Shared;

namespace insightflow_workspace_service.Src.Interface
{
    /// <summary>
    /// Interface para el repositorio de workspaces.
    /// </summary>
    public interface IWorkspaceRepository
    {
        /// <summary>
        /// Crea un nuevo workspace.
        /// </summary>
        /// <param name="createWorkspaceDto">Workspace a crear.</param>
        /// <returns>Resultado de la operación.</returns>
        public Task<Result<bool>> CreateWorkspace(CreateWorkspaceDto createWorkspaceDto);

        /// <summary>
        /// Obtiene todos los workspaces de un usuario.
        /// </summary>
        /// <param name="UserId">Identificador del usuario.</param>
        /// <returns>Lista de workspaces del usuario.</returns>
        public Task<Result<List<WorkspaceByUserDto>>> GetAllWorkspacesByUser(Guid UserId);

        /// <summary>
        /// Obtiene un workspace por su ID.
        /// </summary>
        /// <param name="WorkspaceId">Identificador del workspace.</param>
        /// <returns>Workspace correspondiente al ID.</returns>
        public Task<Result<WorkspaceDto?>> GetWorkspaceById(Guid WorkspaceId);

        /// <summary>
        /// Actualiza un workspace existente.
        /// </summary>
        /// <param name="workspaceId">Identificador del workspace.</param>
        /// <param name="updateWorkspaceDto">Datos para actualizar el workspace.</param>
        /// <returns>Resultado de la operación.</returns>
        public Task<Result<bool>> UpdateWorkspace(Guid workspaceId, UpdateWorkspaceDto updateWorkspaceDto);

        /// <summary>
        /// Elimina un workspace por su ID.
        /// </summary>
        /// <param name="workspaceId">Identificador del workspace.</param>
        /// <returns>Resultado de la operación.</returns>
        public Task<Result<bool>> DeleteWorkspace(Guid workspaceId);

        /// <summary>
        /// Obtiene todos los workspaces.
        /// </summary>
        /// <returns>Lista de todos los workspaces.</returns>
        public Task<Result<List<Workspace>>> GetAllWorkspaces();
    }
}