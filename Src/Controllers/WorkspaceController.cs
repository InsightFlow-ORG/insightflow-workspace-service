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
    [Route("workspace")]
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
            
            var result = await _workspaceRepository.CreateWorkspace(createWorkspaceDto);

            if (!result.IsSuccess)
            {
                return StatusCode(result.StatusCode, new { message = result.ErrorMessage });
            }

            return Ok(result.Data);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllWorkspaces()
        {
            var result = await _workspaceRepository.GetAllWorkspaces();
            return Ok(result.Data);  
        }

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