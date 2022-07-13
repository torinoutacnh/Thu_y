using Microsoft.AspNetCore.Authorization;
using Thu_y.Modules.ReportModule.Model;
using Thu_y.Modules.ReportModule.Ports;
using Thu_y.Utils.Infrastructure.Application.Models;

namespace Thu_y.Modules.ReportModule.Endpoints
{
    public class SealTabEndpoint
    {
        public const string Prefix = "";
        public const string BasePath = Prefix + "/sealtab";
        public const string GetSealTab = BasePath;
        public const string CreateSeal = BasePath + "/create";
        public const string UpdateSeal = BasePath + "/update";
        public const string DeleteSeal = BasePath + "/delete";

    }
    public static class SealTabRoute
    {
        public static IEndpointRouteBuilder MapSealTabEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet(SealTabEndpoint.GetSealTab, [Authorize(AuthenticationSchemes = "Bearer")] (string reportId, ISealTabService sealTabService) =>
            {
                try
                {
                    var seal = sealTabService.GetSealtabByReportId(reportId);

                    return Results.Ok(value: new ResponseModel<ICollection<SealTabModel>>(data: seal));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.Json(new ResponseModel<ICollection<SealTabModel>>(message: ex.Message), statusCode: ex.HResult);
                    }
                    return Results.Json(new ResponseModel<ICollection<SealTabModel>>(message: ex.Message), statusCode: 500);
                }
            }).WithTags(SealTabEndpoint.BasePath);

            endpoints.MapPost(SealTabEndpoint.CreateSeal, [Authorize(AuthenticationSchemes = "Bearer")] async (SealTabModel model, ISealTabService sealTabService) =>
            {
                try
                {
                    var data = sealTabService.CreateAsync(model);
                    return Results.Ok(value: new ResponseModel<string>(data: data.Result));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: ex.HResult);
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }
            }).WithTags(SealTabEndpoint.BasePath);

            endpoints.MapPost(SealTabEndpoint.UpdateSeal, [Authorize(AuthenticationSchemes = "Bearer")] async (UpdateSealTabModel model, ISealTabService sealTabService) =>
            {
                try
                {
                    await sealTabService.UpdateAsync(model);
                    return Results.Ok(value: new ResponseModel<string>(data: "Success"));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: ex.HResult);
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }
            }).WithTags(SealTabEndpoint.BasePath);

            endpoints.MapPost(SealTabEndpoint.DeleteSeal, [Authorize(AuthenticationSchemes = "Bearer")] async (string id, ISealTabService sealTabService) =>
            {
                try
                {
                    await sealTabService.DeleteAsync(id);
                    return Results.Ok(value: new ResponseModel<string>(data: "Success"));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: ex.HResult);
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }

            }).WithTags(SealTabEndpoint.BasePath);

            return endpoints;
        }
    }
}
