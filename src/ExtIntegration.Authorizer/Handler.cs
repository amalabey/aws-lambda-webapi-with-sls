using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace RecipesAPI.Authorizer
{
    public class Function
    {

        public APIGatewayCustomAuthorizerResponse FunctionHandler(APIGatewayCustomAuthorizerRequest apigAuthRequest, ILambdaContext context)
        {
            bool authorized = false;
            string token = apigAuthRequest.AuthorizationToken;
            if(!string.IsNullOrEmpty(token) && token == "letmein")
            {
                authorized = true;
            }

            APIGatewayCustomAuthorizerPolicy policy = new APIGatewayCustomAuthorizerPolicy
            {
                Version = "2012-10-17",
                Statement = new List<APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement>()
            };

            policy.Statement.Add(new APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement
            {
                Action = new HashSet<string>(new string[] { "execute-api:Invoke" }),
                Effect = authorized ? "Allow" : "Deny",
                Resource = new HashSet<string>(new string[] { apigAuthRequest.MethodArn })
            });

            APIGatewayCustomAuthorizerContextOutput contextOutput = new APIGatewayCustomAuthorizerContextOutput();
            contextOutput["User"] = "User";
            contextOutput["Path"] = apigAuthRequest.MethodArn;

            return new APIGatewayCustomAuthorizerResponse
            {
                PrincipalID = "user",
                Context = contextOutput,
                PolicyDocument = policy
            };

        }
    }
}
