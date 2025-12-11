using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.Src.Dtos;
using insightflow_workspace_service.Src.Interface;
using Microsoft.AspNetCore.Mvc;

namespace insightflow_workspace_service.Src.Controllers
{
    /// <summary>
    /// Controlador para la gestión de Workspaces
    /// </summary>
    [Controller]
    [Route("workspace")]
    public class WorkspaceController : ControllerBase
    {
        /// <summary>
        /// Repositorio de Workspaces
        /// </summary>
        private readonly IWorkspaceRepository _workspaceRepository;

        /// <summary>
        /// Constructor del controlador de Workspaces
        /// </summary>
        /// <param name="workspaceRepository">Repositorio de Workspaces</param>
        public WorkspaceController(IWorkspaceRepository workspaceRepository)
        {
            _workspaceRepository = workspaceRepository;
        }

        /// <summary>
        /// Crea un nuevo Workspace
        /// </summary>
        /// <param name="createWorkspaceDto">Workspace a crear</param>
        /// <returns>Resultado de la creación del Workspace</returns>
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateWorkspace([FromForm] CreateWorkspaceDto createWorkspaceDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var result = await _workspaceRepository.CreateWorkspace(createWorkspaceDto);

            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, new { message = result.ErrorMessage });
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Obtiene todos los Workspaces
        /// </summary>
        /// <returns>Lista de todos los Workspaces</returns>
        [HttpGet()]
        public async Task<IActionResult> GetAllWorkspaces()
        {
            var result = await _workspaceRepository.GetAllWorkspaces();
            return Ok(result.Data);  
        }

        /// <summary>
        /// Obtiene todos los Workspaces de un usuario específico
        /// </summary>
        /// <param name="userId">ID del usuario</param>
        /// <returns>Lista de Workspaces del usuario</returns>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetWorkspaces([FromRoute] Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid userId." });
            }

            var result = await _workspaceRepository.GetAllWorkspacesByUser(userId);

            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, new { message = result.ErrorMessage });
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Obtiene un Workspace por su ID
        /// </summary>
        /// <param name="workspaceId">ID del Workspace</param>
        /// <returns>Workspace correspondiente al ID</returns>
        [HttpGet("workspace/{workspaceId}")]
        public async Task<IActionResult> GetWorkspace([FromRoute] Guid workspaceId)
        {
            if (workspaceId == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid workspaceId." });
            }

            var result = await _workspaceRepository.GetWorkspaceById(workspaceId);

            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, new { message = result.ErrorMessage });
            }

            return Ok(result.Data);   
        }

        /// <summary>
        /// Actualiza un Workspace existente
        /// </summary>
        /// <param name="workspaceId">ID del Workspace</param>
        /// <param name="updateWorkspaceDto">Datos para actualizar el Workspace</param>
        /// <returns>Resultado de la actualización del Workspace</returns>
        [HttpPatch("{workspaceId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateWorkspace([FromRoute] Guid workspaceId, [FromForm] UpdateWorkspaceDto updateWorkspaceDto)
        {

            if (ModelState.IsValid == false) return BadRequest(ModelState);

            if (workspaceId == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid workspaceId." });
            }

            if (updateWorkspaceDto.Name == null 
            && updateWorkspaceDto.Image == null
            && updateWorkspaceDto.Description == null
            && updateWorkspaceDto.Theme == null)
            {
                return BadRequest(new { message = "At least one field (Name, Image, Description, or Theme) must be provided for update." });
            }

            var result = await _workspaceRepository.UpdateWorkspace(workspaceId, updateWorkspaceDto);

            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, new { message = result.ErrorMessage });
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Elimina un Workspace por su ID
        /// </summary>
        /// <param name="workspaceId">ID del Workspace</param>
        /// <returns>Resultado de la eliminación del Workspace</returns>
        [HttpDelete("{workspaceId}")]
        public async Task<IActionResult> DeleteWorkspace([FromRoute] Guid workspaceId)
        {
            if (workspaceId == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid workspaceId." });
            }

            var result = await _workspaceRepository.DeleteWorkspace(workspaceId);

            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, new { message = result.ErrorMessage });
            }

            return Ok(result.Data);
        }
    }
}