﻿namespace GainsTrackerAPI.Configurations.Exceptions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
    {
        return applicationBuilder.UseMiddleware<GlobalErrorHandlingMiddleware>();
    }
}
