using Amazon.SQS;
using Amazon.SQS.Model;
using Queues.Interfaces;
using System;
using System.Threading.Tasks;

namespace Queues.SQS
{
    public class BookQueue : IBookQueue
    {
        private const string QueueName = "BookQ";
        private readonly IAmazonSQS _client;

        public BookQueue(IAmazonSQS client)
        {
            _client = client;
        }

        public async Task Push(string isbn)
        {
            var request = new SendMessageRequest
            {
                QueueUrl = await GetQueueURL(),
                MessageBody = $"Book ISBN: {isbn}, {Guid.NewGuid()}"
            };

            await _client.SendMessageAsync(request);
        }

        private async Task<string> GetQueueURL()
        {
            var request = new GetQueueUrlRequest(QueueName);
            var result = await _client.GetQueueUrlAsync(request);
            return result.QueueUrl;
        }
    }
}
