﻿using Microsoft.AspNetCore.Components.Forms;
using System.Net;

namespace EcommerceBlazorWebApp_Server.Service.IService
{
    public interface IFileUpload
    {
        Task<string> UploadFile(IBrowserFile file);
        bool DeleteFile(string filePath);
    }
}
