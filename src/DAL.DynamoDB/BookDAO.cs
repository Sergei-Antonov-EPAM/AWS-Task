using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using DAL.Interfaces;
using Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.DynamoDB
{
    public class BookDAO : IBookDAO
    {
        private const string TableName = "Books";
        private readonly IAmazonDynamoDB _client;

        public BookDAO(IAmazonDynamoDB client)
        {
            _client = client;
        }

        public async Task Add(Book book)
        {
            var request = new PutItemRequest
            {
                TableName = TableName,
                Item = new Dictionary<string, AttributeValue>()
                {
                    { "ISBN", new AttributeValue { S = book.ISBN }},
                    { "Title", new AttributeValue { S = book.Title }},
                    { "Description", new AttributeValue { S = book.Description }},
                }
            };

            await _client.PutItemAsync(request);
        }

        public async Task<Book[]> GetAll()
        {
            var request = new ScanRequest(TableName);
            var response = await _client.ScanAsync(request);
            var result = new List<Book>();

            foreach (var book in response.Items)
            {
                result.Add(Map(book));
            }

            return result.ToArray();
        }

        public async Task<Book> GetByISBN(string isbn)
        {
            var request = new GetItemRequest()
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue>() { { "ISBN", new AttributeValue() { S = isbn } } }
            };

            var response = await _client.GetItemAsync(request);
            var item = response.Item;

            if (!item.ContainsKey("ISBN"))
            {
                return null;
            }

            return Map(item);
        }

        public async Task Remove(string isbn)
        {
            var request = new DeleteItemRequest
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue>() { { "ISBN", new AttributeValue() { S = isbn } } }
            };

            await _client.DeleteItemAsync(request);
        }

        public async Task Update(string isbn, Book book)
        {
            var updates = new Dictionary<string, AttributeValueUpdate>
            {
                ["Title"] = new AttributeValueUpdate()
                {
                    Action = AttributeAction.PUT,
                    Value = new AttributeValue { S = book.Title ?? string.Empty }
                },

                ["Description"] = new AttributeValueUpdate()
                {
                    Action = AttributeAction.PUT,
                    Value = new AttributeValue { S = book.Description ?? string.Empty }
                }
            };

            var request = new UpdateItemRequest
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue>() { { "ISBN", new AttributeValue() { S = isbn } } },
                AttributeUpdates = updates
            };

            await _client.UpdateItemAsync(request);
        }

        private Book Map(Dictionary<string, AttributeValue> book)
        {
            return new Book
            {
                ISBN = book["ISBN"].S,
                Title = book["Title"].S,
                Description = book["Description"].S,
            };
        }
    }
}
