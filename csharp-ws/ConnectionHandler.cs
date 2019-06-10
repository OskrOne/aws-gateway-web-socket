using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using System.Net;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AwsDotnetCsharp
{
    public class ConnectionHandler
    {
        public APIGatewayProxyResponse Connect(APIGatewayProxyRequest request)
        {
            Connection connection = new Connection();
            connection.add(request.RequestContext.ConnectionId);

            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public APIGatewayProxyResponse Disconnect(APIGatewayProxyRequest request) {
            Connection connection = new Connection();
            connection.delete(request.RequestContext.ConnectionId);

            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
