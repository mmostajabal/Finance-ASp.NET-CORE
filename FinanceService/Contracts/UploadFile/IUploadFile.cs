using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceService.Contracts.UploadFile
{
    public  interface IUploadFile
    {
        Task<string> UploadFile(string path, IFormFile formFile);
    }
}
