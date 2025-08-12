using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using NexaSoft.Agro.Application.Storages;
using NexaSoft.Agro.Infrastructure.ConfigSettings;

namespace NexaSoft.Agro.Infrastructure.Repositories;

public class MinioStorageService : IFileStorageService
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;

    public MinioStorageService(IOptions<StorageOptions> options)
    {
        var config = options.Value ?? throw new ArgumentNullException(nameof(options));

        _bucketName = config.Bucket ?? throw new ArgumentNullException("Bucket no configurado");


        //_bucketName = config["Storage:Bucket"] ?? throw new ArgumentNullException("Bucket no configurado");

        _s3Client = new AmazonS3Client(
            config.AccessKey,
            config.SecretKey,
            new AmazonS3Config
            {
                ServiceURL = config.ServiceUrl, // e.g., http://localhost:9000
                ForcePathStyle = true
            }
        );
    }

    public Task<string> GetPresignedUrlAsync(string fileName, int expirationInMinutes)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _bucketName,
            Key = fileName,
            Expires = DateTime.UtcNow.AddMinutes(expirationInMinutes)
        };

        var url = _s3Client.GetPreSignedURL(request);
        return Task.FromResult(url);
    }

    public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken)
    {
        var request = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = fileName,
            InputStream = fileStream,
            ContentType = contentType
        };

        var response = await _s3Client.PutObjectAsync(request, cancellationToken);

        if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            throw new Exception("Error al subir el archivo");

        return $"{_bucketName}/{fileName}";
    }
}
