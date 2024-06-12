using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.SystemTextJson;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Sokoban.DataTypes;
using Sokoban.SokobanSolvingLogic;
using System.Net;
using System.Text;

namespace SokobanSolverWebApiCore.LambdaHandlers
{
    public class SokobanSolverHandler
    {
        [LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
        public async Task<APIGatewayProxyResponse> Solve(APIGatewayProxyRequest apigProxyEvent, ILambdaContext context)
        {
            HeuristicsSolver solver = new HeuristicsSolver();
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(apigProxyEvent.Body)))
            {
                List<SokobanObject> levelObjects = (new DefaultLambdaJsonSerializer()).Deserialize<List<SokobanObject>>(stream);
                var result= await Task.Run(() => solver.Solve(levelObjects, CancellationToken.None));
                return new APIGatewayProxyResponse() {
                    StatusCode = (int)HttpStatusCode.OK,
                    Body = JsonConvert.SerializeObject(result)
                };
            }
        }
    }
}
