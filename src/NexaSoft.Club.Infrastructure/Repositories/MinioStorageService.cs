using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using NexaSoft.Club.Application.Storages;
using NexaSoft.Club.Infrastructure.ConfigSettings;

namespace NexaSoft.Club.Infrastructure.Repositories;

public class MinioStorageService : IFileStorageService
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;

    public MinioStorageService(IOptions<StorageOptions> options)
    {
        var config = options.Value ?? throw new ArgumentNullException(nameof(options));

        _bucketName = config.Bucket ?? throw new ArgumentNullException("Bucket no configurado");


        //_bucketName = config["Storage:Bucket"] ?? throw new ArgumentNullException("Bucket no configurado");
        //Console.WriteLine($"Url=>: {config.ServiceUrl}");

        _s3Client = new AmazonS3Client(
            config.AccessKey,
            config.SecretKey,
            new AmazonS3Config
            {
                ServiceURL = config.ServiceUrl,
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
        //Cambiar para prod comentar esta linea
        url = url.Replace("https://", "http://");


        // Reemplazar localhost con IP local detectada dinámicamente
        /*if (url.Contains("localhost"))
        {
            var localIp = GetLocalIPAddress();
            url = url.Replace("https://localhost", "http://"+localIp);
        }*/

        

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

        //return $"{_bucketName}/{fileName}";
        return fileName;
    }

    public async Task DeleteAsync(string fileName, CancellationToken cancellationToken)
    {
        try
        {
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName
            };

            var response = await _s3Client.DeleteObjectAsync(deleteRequest, cancellationToken);

            //_logger.LogInformation("Archivo eliminado exitosamente: {FileName}", fileName);
        }
        catch (AmazonS3Exception ex) when (ex.ErrorCode == "NoSuchKey")
        {
            //_logger.LogWarning("El archivo no existe en MinIO: {FileName}", fileName);
            // No lanzamos excepción si el archivo no existe
        }
        catch (AmazonS3Exception ex)
        {
            //_logger.LogError(ex, "Error de Amazon S3 al eliminar archivo: {FileName}. ErrorCode: {ErrorCode}",  fileName, ex.ErrorCode);
            throw new Exception($"Error eliminando archivo de MinIO: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "Error inesperado al eliminar archivo: {FileName}", fileName);
            throw new Exception($"Error inesperado eliminando archivo: {ex.Message}", ex);
        }
    }

    public async Task<bool> ExistsAsync(string fileName, CancellationToken cancellationToken)
    {
        try
        {
            var request = new GetObjectMetadataRequest
            {
                BucketName = _bucketName,
                Key = fileName
            };

            await _s3Client.GetObjectMetadataAsync(request, cancellationToken);
            return true;
        }
        catch (AmazonS3Exception ex) when (ex.ErrorCode == "NoSuchKey" || ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }
    }

    // solo para desarrollo local
    /*private string GetLocalIPAddress()
    {
        var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && !System.Net.IPAddress.IsLoopback(ip))
            {
                return ip.ToString(); // Ej: 192.168.1.100
            }
        }

        throw new Exception("No se pudo encontrar una IP local válida.");
    }*/
}
