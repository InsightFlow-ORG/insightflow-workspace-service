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
    }
}