import * as cdk from 'aws-cdk-lib';
import { Construct } from 'constructs';
import * as lambda from 'aws-cdk-lib/aws-lambda';
import * as apigateway from 'aws-cdk-lib/aws-apigateway';

export class SokobanCdkStack extends cdk.Stack {
  constructor(scope: Construct, id: string, props?: cdk.StackProps) {
    super(scope, id, props);

    const sokobanSolveFunction = new lambda.Function(this, 'SokobanSolveFunction', {
      runtime: lambda.Runtime.DOTNET_6, // Choose any supported Node.js runtime
      code: lambda.Code.fromAsset('../SokobanSolverWebApiCore/bin/Release/net6.0'), // Points to the lambda directory
      handler: 'SokobanSolverWebApiCore::SokobanSolverWebApiCore.LambdaHandlers.SokobanSolverHandler::Solve', // Points to the 'hello' file in the lambda directory
    });

     // Define the API Gateway resource
     const api = new apigateway.LambdaRestApi(this, 'SokobanSolveApi', {
      handler: sokobanSolveFunction,
      proxy: false,
    });

    const solveResource = api.root.addResource('solve');
    solveResource.addMethod('POST');
  }
}
