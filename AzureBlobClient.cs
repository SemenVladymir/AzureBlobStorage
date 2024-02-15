using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureBlobStorage
{
    internal class AzureBlobClient
    {
        private readonly string connectionString = "DefaultEndpointsProtocol=https;AccountName=semen2024storage;AccountKey=WXlcCpF8jKhWhtFTMFJHTkVRCJBCDqtKmhce0IUpD+5RtLTIbaOADese66YmafU/sTc/AMew666k+AStFqoU1A==;EndpointSuffix=core.windows.net";
        private static string containerName = "myfiles";
        private static BlobServiceClient serviceClient;
        private static AzureBlobClient _instance;
        private static BlobContainerClient containerClient;

        private AzureBlobClient()
        {
            serviceClient = new BlobServiceClient(connectionString);
            containerClient = serviceClient.GetBlobContainerClient(containerName);
        }

        public static AzureBlobClient GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AzureBlobClient();
            }
            return _instance;
        }
        public static async Task UploadBlob()
        {
            var path = "";
            var fileName = "Testfile.txt";
            var localFile = Path.Combine(path, fileName);
            var blobClient = containerClient.GetBlobClient(fileName);
            Console.WriteLine("Uploading to Blob storage");
            using FileStream uploadFileStream = File.OpenRead(localFile);
            await blobClient.UploadAsync(uploadFileStream, true);
            uploadFileStream.Close();
        }

        public static async Task AddBlobFile()
        {
            Console.WriteLine($"Add file to blob");
            string localPath = "C:\\Users\\User\\OneDrive\\Documents\\GitHub\\AzureBlobStorage\\bin\\Debug\\net6.0";
            string fileName = "NewFile.txt";
            string localFilePath = Path.Combine(localPath, fileName);
            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(localFilePath, true);
        }

        public static async Task DeleteBlob()
        {
            Console.Write("Press any key to begin clean up");
            Console.ReadLine();
            Console.WriteLine("Deleting blob container...");
            await containerClient.DeleteAsync();
            Console.WriteLine("Done");
        }


        public static async Task ShowFilesList()
        {
            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                Console.WriteLine("\t" + blobItem.Name);
            }
        }

    }
}
