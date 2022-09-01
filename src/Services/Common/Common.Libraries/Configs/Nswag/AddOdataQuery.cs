using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using NSwag;
using NSwag.Generation.AspNetCore;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace Common.Libraries.Nswag;
[ExcludeFromCodeCoverage]
public class AddOdataQuery: IOperationProcessor
{
    public bool Process(OperationProcessorContext context)
    {
        var apiDescription = ((AspNetCoreOperationProcessorContext)context).ApiDescription;
        var endpointMetadata = apiDescription.ActionDescriptor.EndpointMetadata;

        if (endpointMetadata is null)
            return true;

        if (endpointMetadata.OfType<ControllerAttribute>().Any() && (endpointMetadata.OfType<EnableQueryAttribute>().Any()))
        {
            if (apiDescription.ParameterDescriptions.Any(o => o.Type?.BaseType != null && o.Type.BaseType.Equals(typeof(ODataQueryOptions))) &&
                context.OperationDescription.Operation.Parameters.Any())
            {
                var param = context.OperationDescription.Operation.Parameters.First();

                context.OperationDescription.Operation.Parameters.Remove(param);
            }

            if (!string.IsNullOrEmpty(apiDescription.RelativePath) && apiDescription.RelativePath.Contains("/$count"))
            {
                return true;
            }

            context.OperationDescription.Operation.Parameters.Add(new OpenApiParameter
            {
                Name = "$count",
                Kind = OpenApiParameterKind.Query,
                Type = NJsonSchema.JsonObjectType.Boolean,
                Default = true,
            });

            context.OperationDescription.Operation.Parameters.Add(new OpenApiParameter
            {
                Name = "$skip",
                Kind = OpenApiParameterKind.Query,
                Type = NJsonSchema.JsonObjectType.Integer,
                Default = 0,
            });

            context.OperationDescription.Operation.Parameters.Add(new OpenApiParameter
            {
                Name = "$top",
                Kind = OpenApiParameterKind.Query,
                Type = NJsonSchema.JsonObjectType.Integer,
                Default = 5,
            });

            context.OperationDescription.Operation.Parameters.Add(new OpenApiParameter
            {
                Name = "$filter",
                Kind = OpenApiParameterKind.Query,
                Type = NJsonSchema.JsonObjectType.String,
            });


            context.OperationDescription.Operation.Parameters.Add(new OpenApiParameter
            {
                Name = "$orderby",
                Kind = OpenApiParameterKind.Query,
                Type = NJsonSchema.JsonObjectType.String,
            });

            context.OperationDescription.Operation.Parameters.Add(new OpenApiParameter
            {
                Name = "$search",
                Kind = OpenApiParameterKind.Query,
                Type = NJsonSchema.JsonObjectType.String
            });

            context.OperationDescription.Operation.Parameters.Add(new OpenApiParameter
            {
                Name = "$compute",
                Kind = OpenApiParameterKind.Query,
                Type = NJsonSchema.JsonObjectType.String
            });

            context.OperationDescription.Operation.Parameters.Add(new OpenApiParameter
            {
                Name = "$select",
                Kind = OpenApiParameterKind.Query,
                Type = NJsonSchema.JsonObjectType.String,
            });

            context.OperationDescription.Operation.Parameters.Add(new OpenApiParameter
            {
                Name = "$expand",
                Kind = OpenApiParameterKind.Query,
                Type = NJsonSchema.JsonObjectType.String,
            });
        }

        return true;
    }
}