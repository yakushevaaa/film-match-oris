using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace S3
{
    public class MinioLifecycleService : IHostedService
    {
        private readonly IMinioClient _minioClient;
        private readonly S3Options _s3Options;

        public MinioLifecycleService(IMinioClient minioClient, IOptions<S3Options> s3Options)
        {
            _minioClient = minioClient;
            _s3Options = s3Options.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var bucketName = _s3Options.BucketName;

            bool exists = await _minioClient.BucketExistsAsync(
                new BucketExistsArgs().WithBucket(bucketName), cancellationToken);

            if (!exists)
            {
                await _minioClient.MakeBucketAsync(
                    new MakeBucketArgs().WithBucket(bucketName), cancellationToken);

                Console.WriteLine($"Bucket '{bucketName}' created successfully.");
            }
            else
            {
                Console.WriteLine($"Bucket '{bucketName}' already exists.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}