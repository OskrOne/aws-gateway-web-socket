# aws-gateway-web-socket
Performance test of AWS API Gateway with Web Socket using C#

## Usage ##

The example was created with Serverless Framework, AWS DynamoDB, AWS API Gateway, AWS Lambda & C# as language

### Create tables Connection & Assets ###

```sh
aws dynamodb create-table --table-name Assets --attribute-definitions AttributeName=ContractId,AttributeType=S \
--key-schema AttributeName=ContractId,KeyType=HASH --provisioned-throughput ReadCapacityUnits=5,WriteCapacityUnits=5

aws dynamodb create-table --table-name Connection --attribute-definitions AttributeName=ConnectionId,AttributeType=S \
--key-schema AttributeName=ConnectionId,KeyType=HASH --provisioned-throughput ReadCapacityUnits=5,WriteCapacityUnits=5
```

### Build application ###

```sh
cd csharp-ws/
./build.sh
```

**Deploy application**
```sh
sls deploy
```

### Create API Gateway ###

To create a WebSocket API using the API Gateway console

1. Sign in to the API Gateway console and choose Create API.
1. Under Choose the protocol, choose WebSocket.
1. Under Create a new API, choose New API.
1. Under Settings, in the API name field, type the name of your API, for example, AssetApi.
1. Enter a Route Selection Expression for the API: $request.body.action.
1. Choose Create API.
1. Select $connect route, select LambdaFunction as IntegrationType and then select csharp-ws-dev-connect
1. Select $disconnect route, select LambdaFunction as IntegrationType and then select csharp-ws-dev-disconnect 
1. Add new route key: message, select LambdaFunction as IntegrationType and then select csharp-ws-dev-message
1. Click on actions and then on Deploy API



### Test application ###
