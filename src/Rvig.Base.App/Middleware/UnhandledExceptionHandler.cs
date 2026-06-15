using Brp.Shared.Infrastructure.Http;
using Brp.Shared.Infrastructure.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rvig.HaalCentraalApi.Shared.Exceptions;
using Serilog;
using System.Linq;

namespace Rvig.Base.App.Middleware;

public class UnhandledExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly IDiagnosticContext _diagnosticContext;

    public UnhandledExceptionHandler(RequestDelegate next, IDiagnosticContext diagnosticContext)
    {
        _next = next;
        _diagnosticContext = diagnosticContext;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (InvalidParamsException ex)
        {
            _diagnosticContext.SetException(ex);

            await httpContext.Response.WriteProblemDetailsAsync(CreateProblemDetails(ex));
        }
        catch (TooManyResultsException ex)
        {
            _diagnosticContext.SetException(ex);

            await httpContext.Response.WriteProblemDetailsAsync(CreateProblemDetails(ex));
        }
        catch (Exception ex)
        {
            _diagnosticContext.SetException(ex);

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.HandleInternalServerError();
        }
    }

    private static ProblemDetails CreateProblemDetails(TooManyResultsException ex)
    {
        return new ProblemDetails
        {
            Title = ex.Title,
            Detail = ex.Details,
            Status = 400,
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            Instance = "/haalcentraal/api/brp/personen",
            Extensions = { ["code"] = ex.ErrorCode.ToString() }
        };
    }

    private static ProblemDetails CreateProblemDetails(InvalidParamsException ex)
    {
        var invalidParams = (from param in ex.InvalidParams
                             select new
                             {
                                 name = param.Name,
                                 code = param.Code,
                                 reason = param.Reason
                             }).ToList();

        return new ProblemDetails
        {
            Title = ex.Title,
            Detail = ex.Details,
            Status = 400,
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            Instance = "/haalcentraal/api/brp/personen",
            Extensions = { ["invalidParams"] = invalidParams, ["code"] = ex.ErrorCode.ToString() }
        };
    }
}
