using Microsoft.AspNetCore.Http;
using RestWithASPNET.Data.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNET.Business.Implementations
{
    public class FileBusinessImplementation : IFileBusiness
    {
        private readonly string _basePath;
        private readonly IHttpContextAccessor _context;

        public FileBusinessImplementation(IHttpContextAccessor context)
        {
            _context = context;
            _basePath = Directory.GetCurrentDirectory() + "\\UploadDir\\";
        }
        public byte[] GetFile(string fileName)
        {
            var filePath =_basePath + fileName;
            return File.ReadAllBytes(filePath);
        }

        public async Task<FileDetailDTO> SaveFileToDisk(IFormFile file)
        {
            FileDetailDTO fileDetailt = new FileDetailDTO();

            var fileType = Path.GetExtension(file.FileName);
            var baseUrl = _context.HttpContext.Request.Host;

            if (fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" ||
                fileType.ToLower() == ".jpeg" || fileType.ToLower() == ".png")
            {
                var docName = Path.GetFileName(file.FileName);

                if (file != null && file.Length > 0)
                {
                    var destination = Path.Combine(_basePath, "", docName);
                    fileDetailt.DocumentName = docName;
                    fileDetailt.DocType = fileType;
                    fileDetailt.DocUrl = Path.Combine(baseUrl + "/api/file/v1/" + fileDetailt.DocumentName);

                    using var stream = new FileStream(destination, FileMode.Create);
                    await file.CopyToAsync(stream);
                }
            }
            return fileDetailt;
        }

        public async Task<List<FileDetailDTO>> SaveFilesToDisk(IList<IFormFile> files)
        {
            List<FileDetailDTO> list = new List<FileDetailDTO>();

            foreach (var file in files)
                list.Add(await SaveFileToDisk(file));

            return list;
        }
    }
}
