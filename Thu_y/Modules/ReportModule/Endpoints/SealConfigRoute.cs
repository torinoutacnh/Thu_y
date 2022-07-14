using Microsoft.AspNetCore.Authorization;
using Thu_y.Modules.ReportModule.Model;
using Thu_y.Modules.ReportModule.Ports;
using Thu_y.Utils.Infrastructure.Application.Models;

namespace Thu_y.Modules.ReportModule.Endpoints
{
    public class SealConfigEndpoint
    {
        public const string Prefix = "";
        public const string BasePath = Prefix + "/sealconfig";
        public const string GetConfig = BasePath;
        public const string CreateConfig = BasePath + "/create";
        public const string UpdateConfig = BasePath + "/update";
        public const string DeleteConfig = BasePath + "/delete";

    }
    public static class SealConfigRoute
    {
        public static IEndpointRouteBuilder MapSealConfigEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet(SealConfigEndpoint.GetConfig, [Authorize(AuthenticationSchemes = "Bearer")] (ISealConfigService sealConfigService) =>
            {
                try
                {
                    var seal = sealConfigService.GetListSealConfig();

                    return Results.Ok(value: new ResponseModel<ICollection<SealConfigModel>>(data: seal));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.Json(new ResponseModel<SealConfigModel>(message: ex.Message), statusCode: ex.HResult);
                    }
                    return Results.Json(new ResponseModel<SealConfigModel>(message: ex.Message), statusCode: 500);
                }
            }).WithTags(SealConfigEndpoint.BasePath);

            endpoints.MapPost(SealConfigEndpoint.CreateConfig, [Authorize(AuthenticationSchemes = "Bearer")] async (SealConfigModel model, ISealConfigService sealConfigService) =>
            {
                try
                {
                    await sealConfigService.CreateAsync(model);
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
            }).WithTags(SealConfigEndpoint.BasePath);

            endpoints.MapPost(SealConfigEndpoint.UpdateConfig, [Authorize(AuthenticationSchemes = "Bearer")] async (SealConfigModel model, ISealConfigService sealConfigService) =>
            {
                try
                {
                    await sealConfigService.UpdateAsync(model);
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
            }).WithTags(SealConfigEndpoint.BasePath);

            endpoints.MapPost(SealConfigEndpoint.DeleteConfig, [Authorize(AuthenticationSchemes = "Bearer")] async (DeleteModel request, ISealConfigService sealConfigService) =>
            {
                try
                {
                    await sealConfigService.DeleteAsync(request.Id);
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

            }).WithTags(SealConfigEndpoint.BasePath);

            return endpoints;
        }
    }
}
