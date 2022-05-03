using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag;
using NSwag.Generation;
using NSwag.Generation.AspNetCore;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;
using NSwag.Generation.Processors.Security;

namespace Catalog.API.Infrastructures.Nswag;

[ExcludeFromCodeCoverage]
public static class CombineAuthWithFastEndpoints
{
    public static void AddAuthController(this AspNetCoreOpenApiDocumentGeneratorSettings settings)
    {
        settings.AddAuth("JWTBearerAuth", new OpenApiSecurityScheme
        {
            Type = OpenApiSecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            Description = "Enter a JWT token to authorize the requests..."
        });
    }

    public static OpenApiDocumentGeneratorSettings AddAuth(this OpenApiDocumentGeneratorSettings s, string schemeName, OpenApiSecurityScheme securityScheme, IEnumerable<string>? globalScopeNames = null)
    {
        if (globalScopeNames is null)
            s.DocumentProcessors.Add(new SecurityDefinitionAppender(schemeName, securityScheme));
        else
            s.DocumentProcessors.Add(new SecurityDefinitionAppender(schemeName, globalScopeNames, securityScheme));

        s.OperationProcessors.Add(new OperationSecurityProcessor(schemeName));

        return s;
    }
}

[ExcludeFromCodeCoverage]
internal class OperationSecurityProcessor : IOperationProcessor
{
    private readonly string schemeName;

    public OperationSecurityProcessor(string schemeName)
        => this.schemeName = schemeName;

    public bool Process(OperationProcessorContext context)
    {
        var epMeta = ((AspNetCoreOperationProcessorContext)context).ApiDescription.ActionDescriptor.EndpointMetadata;

        if (epMeta is null)
            return true;

        if (!epMeta.OfType<ControllerAttribute>().Any())
        {
            if ((epMeta.OfType<AllowAnonymousAttribute>().Any() || !epMeta.OfType<AuthorizeAttribute>().Any()))
            {
                return true;
            }

            var epSchemes = epMeta.OfType<EndpointDefinition>().Single().AuthSchemes;
            if (epSchemes?.Contains(schemeName) == false)
            {
                return true;
            }
        }

        if (context.OperationDescription.Operation.Security == null) {
            context.OperationDescription.Operation.Security = new List<OpenApiSecurityRequirement>();
        }

        context.OperationDescription.Operation.Security.Add(new OpenApiSecurityRequirement
        {
            { schemeName, BuildScopes(epMeta!.OfType<AuthorizeAttribute>()) }
        });

        return true;
    }

    private static IEnumerable<string> BuildScopes(IEnumerable<AuthorizeAttribute> authorizeAttributes)
    {
        return authorizeAttributes
            .Where(a => a.Roles != null)
            .SelectMany(a => a.Roles!.Split(','))
            .Distinct();
    }
}
