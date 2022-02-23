using Microsoft.AspNetCore.Http;
using RestWithASPNET.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestWithASPNET.Business
{
    public interface IFileBusiness
    {
        public byte[] GetFile(string fileName);
        public Task<FileDetailDTO> SaveFileToDisk(IFormFile file);
        public Task<List<FileDetailDTO>> SaveFilesToDisk(IList<IFormFile> file);
    }
}
