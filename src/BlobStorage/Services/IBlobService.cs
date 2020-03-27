using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlobStorage.Services
{
    public interface IBlobService
    {
        Task<IEnumerable<string>> GetAllAsync();

        Task<Models.BlobInfo> GetAsync(string name);

        Task UploadFileAsync(string filePath, string fileName);

        Task UploadContentAsync(string content, string fileName);

        Task DeleteAsync(string fileName);
    }
}
