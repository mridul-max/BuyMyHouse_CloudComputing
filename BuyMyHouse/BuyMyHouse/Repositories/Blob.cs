using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace BuyMyHouse.Repositories
{
    public class Blob : IBlob
    {
        private async Task<CloudBlobContainer> GetContainer()
        {
            var connectionstring = Environment.GetEnvironmentVariable("BlobStorage");
            var storageAccount = CloudStorageAccount.Parse(connectionstring);
            var serviceClient = storageAccount.CreateCloudBlobClient();
            var container = serviceClient.GetContainerReference(
                    $"{Environment.GetEnvironmentVariable("FileContainer")}"
            );
            await container.CreateIfNotExistsAsync();
            return container;
        }

        public async Task<string> GetBlob(string file)
        {
            var containerClient = await GetContainer();
            var blob = containerClient.GetBlockBlobReference($"{file}");
            return blob.StorageUri.PrimaryUri.ToString();
        }
        public async Task<string> CreateFile(string file, string referenceFile)
        {
            var containerClient = await GetContainer();
            var blobReference = containerClient.GetBlockBlobReference(referenceFile);
            var bytes = FileDecode(file);
            using (var stream = new MemoryStream(bytes))
            {
                await blobReference.UploadFromStreamAsync(stream);
            }
            return $" this is the refrence file {referenceFile}";
        }
        public async Task<bool> DeleteBlob(string file)
        {
            var containerClient = await GetContainer();
            var blob = containerClient.GetBlockBlobReference($"{file}");
            var isDeleted = await blob.DeleteIfExistsAsync();
            return isDeleted;
        }

        public static byte[] FileDecode(string input)
        {
            var output = input;
            output = output.Replace('-', '+');
            output = output.Replace('_', '/');
            switch (output.Length % 4)
            {
                case 0:
                    break;
                case 2:
                    output += "==";
                    break;
                case 3:
                    output += "=";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(input), "Illegal base64url string!");
            }

            var converted = Convert.FromBase64String(output);

            return converted;
        }
    }
}
