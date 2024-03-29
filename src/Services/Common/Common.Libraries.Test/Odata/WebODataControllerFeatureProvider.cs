using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Common.Libraries.Test.Odata;

/// <summary>
/// Controller feature provider
/// </summary>
public class WebODataControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>, IApplicationFeatureProvider
{
    private Type[] _controllers;

    /// <summary>
    /// Initializes a new instance of the <see cref="WebODataControllerFeatureProvider"/> class.
    /// </summary>
    /// <param name="controllers">The controllers</param>
    public WebODataControllerFeatureProvider(params Type[] controllers)
    {
        _controllers = controllers;
    }

    /// <summary>
    /// Updates the feature instance.
    /// </summary>
    /// <param name="parts">The list of <see cref="ApplicationPart" /> instances in the application.</param>
    /// <param name="feature">The controller feature.</param>
    public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
    {
        if (_controllers == null)
        {
            return;
        }

        feature.Controllers.Clear();
        foreach (var type in _controllers)
        {
            feature.Controllers.Add(type.GetTypeInfo());
        }
    }
}