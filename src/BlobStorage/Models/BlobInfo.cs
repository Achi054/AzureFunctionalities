using System.IO;

namespace BlobStorage.Models
{
    public class BlobInfo
    {
        public Stream Content { get; }
        public string ContentType { get; }

        public BlobInfo(Stream content, string contentType)
            => (Content, ContentType) = (content, contentType);
    }
}
