using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using insightflow_workspace_service.Src.Dtos;
using insightflow_workspace_service.Src.Interface;
using Microsoft.AspNetCore.Mvc;

namespace insightflow_workspace_service.Src.Controllers
{
    [Controller]
    [Route("workspaces")]
    public class WorkspaceController : ControllerBase
    {
        private readonly IWorkspaceRepository _workspaceRepository;

        public WorkspaceController(IWorkspaceRepository workspaceRepository)
        {
            _workspaceRepository = workspaceRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkspace([FromForm] CreateWorkspaceDto createWorkspaceDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            try
            {
                var response = await _workspaceRepository.CreateWorkspace(createWorkspaceDto);

                if (response == false)
                {
                    return BadRequest(new { message = "Failed to create workspace." });
                }

                return Ok(new { success = true });

            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("workspaces/{userId}")]
        public async Task<IActionResult> GetWorkspaces([FromRoute] Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid userId." });
            }

            try
            {
                var workspaces = await _workspaceRepository.GetAllWorkspacesByUser(userId);
                return Ok(workspaces);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("workspace/{workspaceId}")]
        public async Task<IActionResult> GetWorkspace([FromRoute] Guid workspaceId)
        {
            if (workspaceId == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid workspaceId." });
            }

            try
            {
                var workspace = await _workspaceRepository.GetWorkspaceById(workspaceId);

                if (workspace == null)
                {
                    return NotFound(new { message = "Workspace not found." });
                }

                return Ok(workspace);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("workspaces/{workspaceId}")]
        public async Task<IActionResult> UpdateWorkspace([FromRoute] Guid workspaceId, [FromForm] UpdateWorkspaceDto updateWorkspaceDto)
        {

            if (ModelState.IsValid == false) return BadRequest(ModelState);

            if (workspaceId == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid workspaceId." });
            }

            if (updateWorkspaceDto.Name == null && updateWorkspaceDto.Image == null)
            {
                return BadRequest(new { message = "At least one field (Name or Image) must be provided for update." });
            }

            try
            {
                var response = await _workspaceRepository.UpdateWorkspace(workspaceId, updateWorkspaceDto);

                if (response == false)
                {
                    return NotFound(new { message = "Error updating workspace." });
                }

                return Ok(new { success = true });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("workspaces/{workspaceId}")]
        public async Task<IActionResult> DeleteWorkspace([FromRoute] Guid workspaceId)
        {
            if (workspaceId == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid workspaceId." });
            }

            try
            {
                var response = await _workspaceRepository.DeleteWorkspace(workspaceId);

                if (response == false)
                {
                    return NotFound(new { message = "Error deleting workspace." });
                }

                return Ok(new { success = true });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}