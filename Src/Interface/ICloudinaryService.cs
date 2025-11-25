using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace insightflow_workspace_service.Src.Interface
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}