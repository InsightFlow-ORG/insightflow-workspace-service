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
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateWorkspace([FromForm] CreateWorkspaceDto createWorkspaceDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var response = await _workspaceRepository.CreateWorkspace(createWorkspaceDto);

            if (response == false)
            {
                return BadRequest(new { message = "A workspace with the same name already exists." });
            }

            return Ok(new { success = true });
        }

        [HttpGet("workspaces")]
        public async Task<IActionResult> GetAllWorkspaces()
        {
            var workspaces = await _workspaceRepository.GetAllWorkspaces();
            return Ok(workspaces);  
        }

        [HttpGet("workspaces/{userId}")]
        public async Task<IActionResult> GetWorkspaces([FromRoute] Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid userId." });
            }

            var workspaces = await _workspaceRepository.GetAllWorkspacesByUser(userId);
            return Ok(workspaces);
        }

        [HttpGet("workspace/{workspaceId}")]
        public async Task<IActionResult> GetWorkspace([FromRoute] Guid workspaceId)
        {
            if (workspaceId == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid workspaceId." });
            }

            var workspace = await _workspaceRepository.GetWorkspaceById(workspaceId);

            if (workspace == null)
            {
                return NotFound(new { message = "Workspace not found." });
            }

            return Ok(workspace);
        }

        [HttpPatch("workspaces/{workspaceId}")]
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

            var response = await _workspaceRepository.UpdateWorkspace(workspaceId, updateWorkspaceDto);

            if (response == false)
            {
                return BadRequest(new { message = "Error updating workspace. Possible reasons: workspace not found, inactive workspace, duplicate name, or no changes detected." });
            }

            return Ok(new { success = true });
        }

        [HttpDelete("workspaces/{workspaceId}")]
        public async Task<IActionResult> DeleteWorkspace([FromRoute] Guid workspaceId)
        {
            if (workspaceId == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid workspaceId." });
            }

            var response = await _workspaceRepository.DeleteWorkspace(workspaceId);

            if (response == false)
            {
                return NotFound(new { message = "Error deleting workspace." });
            }

            return Ok(new { success = true });
        }
    }
}