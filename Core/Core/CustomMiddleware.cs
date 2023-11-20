﻿using Core.Services;
using Microsoft.Extensions.Options;

namespace Core;

public class CustomMiddleware2
{
    private readonly RequestDelegate _next;
    private readonly IResponseFormatter _formatter;

    public CustomMiddleware2(RequestDelegate next, IResponseFormatter formatter)
    {
        _next = next;
        _formatter = formatter;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path == "/middleware2")
        {
            await _formatter.Format(context, "Custom Middleware 2");
        }
        else
        {
            await _next(context);
        }
    }
}
