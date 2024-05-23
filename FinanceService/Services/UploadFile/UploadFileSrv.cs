using FinanceService.Contracts.UploadFile;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceService.Services.UploadFile
{
    public class UploadFileSrv : IUploadFile
    {
        /// <summary>
        /// UploadFile
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        /// <returns>File Name</returns>
        public async Task<string> UploadFile(string path, IFormFile file)
        {

            string fileName = Path.GetFileName(file.FileName);
            string fileSavePath = Path.Combine(path, fileName);
            using(FileStream fileStream = new FileStream(fileSavePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return fileName;
        }
    }
}
