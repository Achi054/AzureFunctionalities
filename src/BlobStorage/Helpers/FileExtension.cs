using Microsoft.AspNetCore.StaticFiles;

namespace BlobStorage.Helpers
{
    public static class FileExtension
    {
        private static readonly FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();

        public static string GetContentType(this string file)
        {
            if (!provider.TryGetContentType(file, out string contentType))
            {
                return "application/octet-stream";
            }

            return contentType;
        }
    }
}
