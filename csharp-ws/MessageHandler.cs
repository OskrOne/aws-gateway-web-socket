using Amazon.ApiGatewayManagementApi;
using Amazon.ApiGatewayManagementApi.Model;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace AwsDotnetCsharp
{
    class MessageHandler
    {
        public APIGatewayProxyResponse Message(APIGatewayProxyRequest request)
        {
            Console.WriteLine("ConnectionId: " + request.RequestContext.ConnectionId);
            Request finalRequest = JsonConvert.DeserializeObject<Request>(request.Body);

            Asset asset = new Asset();
            Document document = asset.getItem(finalRequest.ContractId);
            AmazonApiGatewayManagementApiClient client = new AmazonApiGatewayManagementApiClient(new AmazonApiGatewayManagementApiConfig()
            {
                ServiceURL = "https://" + request.RequestContext.DomainName + "/" + request.RequestContext.Stage
            });

            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(document)));
            PostToConnectionRequest postRequest = new PostToConnectionRequest()
            {
                ConnectionId = request.RequestContext.ConnectionId,
                Data = stream
            };

            var result = client.PostToConnectionAsync(postRequest);
            result.Wait();

            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }

    public class Request
    {
        public string ContractId { get; set; }
        public Request(string contractId)
        {
            ContractId = contractId;
        }

        public override string ToString()
        {
            return "ContractId: " + ContractId;
        }
    }
}
