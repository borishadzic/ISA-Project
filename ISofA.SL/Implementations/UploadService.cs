using ISofA.SL.Services;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ISofA.SL.Implementations
{
    public class UploadService : IUploadService
    {
        private CloudBlobContainer _cloudBlobContainer;

        public UploadService()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            _cloudBlobContainer = blobClient.GetContainerReference("img");
            _cloudBlobContainer.CreateIfNotExists();

            BlobContainerPermissions permissions = _cloudBlobContainer.GetPermissions();
            permissions.PublicAccess = BlobContainerPublicAccessType.Container;
            _cloudBlobContainer.SetPermissions(permissions);
        }

        public async Task<string> UploadImageAsync(HttpPostedFile image)
        {
            if (image == null)
            {
                return null;
            }

            CloudBlockBlob imageBlob = _cloudBlobContainer.GetBlockBlobReference(
                blobName: Guid.NewGuid() + Path.GetExtension(image.FileName)
            );
            imageBlob.Properties.ContentType = image.ContentType;
            await imageBlob.UploadFromStreamAsync(image.InputStream);

            return imageBlob.Uri.AbsoluteUri;
        }
    }
}
