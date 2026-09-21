using Amazon.S3;
using Amazon.S3.Model;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => "ECS Fargate is running!");

app.MapGet("/s3-test", async () =>
{
    try
    {
        var s3 = new AmazonS3Client();

        var request = new PutObjectRequest
        {
            BucketName = "ecs-practice-logs-033026138018",
            Key = "ecs-test.txt",
            ContentBody = "S3 write test from ECS Fargate"
        };

        await s3.PutObjectAsync(request);

        return Results.Ok("Successfully wrote file to S3!");
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.Run();
