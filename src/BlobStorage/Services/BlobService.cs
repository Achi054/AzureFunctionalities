using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlobStorage.Helpers;
using BlobStorage.Settings;
using Microsoft.Extensions.Logging;

namespace BlobStorage.Services
{
    public class BlobService : IBlobService
    {
        private readonly ILogger<BlobService> _logger;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly AzureStorage _azureStorage;

        public BlobService(ILogger<BlobService> logger, BlobServiceClient blobServiceClient, AzureStorage azureStorage)
            => (_logger, _blobServiceClient, _azureStorage) = (logger, blobServiceClient, azureStorage);

        public async Task DeleteAsync(string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_azureStorage.Container);
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<IEnumerable<string>> GetAllAsync()
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_azureStorage.Container);
            var items = new List<string>();

            await foreach (var item in containerClient.GetBlobsAsync())
            {
                items.Add(item.Name);
            }

            return items;
        }

        public async Task<Models.BlobInfo> GetAsync(string name)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_azureStorage.Container);
            var blobClient = containerClient.GetBlobClient(name);
            var blobDownloadInfo = await blobClient.DownloadAsync();

            return new Models.BlobInfo(blobDownloadInfo.Value.Content, blobDownloadInfo.Value.ContentType);
        }

        public async Task UploadContentAsync(string content, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_azureStorage.Container);
            var blobClient = containerClient.GetBlobClient(fileName);
            var encodedString = Encoding.UTF8.GetBytes(content);
            await blobClient.UploadAsync(new MemoryStream(encodedString));
        }

        public async Task UploadFileAsync(string filePath, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_azureStorage.Container);
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(filePath, new BlobHttpHeaders { ContentType = filePath.GetContentType() });
        }
    }
}
