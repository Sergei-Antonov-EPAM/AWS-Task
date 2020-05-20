using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace S3Lambda
{
    public class Function
    {
        private const string bucketName = "book-s3";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast2;
        private static IAmazonS3 s3Client;

        /// <summary>
        /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
        /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
        /// region the Lambda function is executed in.
        /// </summary>
        public Function()
        {
            s3Client = new AmazonS3Client(bucketRegion);
        }

        /// <summary>
        /// This method is called for every Lambda invocation. This method takes in an SQS event object and can be used 
        /// to respond to SQS messages.
        /// </summary>
        public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
        {
            if (evnt?.Records == null)
            {
                context.Logger.LogLine("Nothing to process.");
                return;
            }

            foreach(var message in evnt.Records)
            {
                await ProcessMessageAsync(message, context);
            }
        }

        private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
        {
            context.Logger.LogLine($"Dequeued message '{message.Body}'");
            await PutS3Object(context, message.Body);
        }

        public static async Task<bool> PutS3Object(ILambdaContext context, string content)
        {
            try
            {
                using (var client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1))
                {
                    var request = new PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = $"book_info_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}",
                        ContentBody = content
                    };
                    var response = await client.PutObjectAsync(request);
                }
                return true;
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"Exception in PutS3Object: '{ex.Message}'");
                return false;
            }
        }
    }
}
