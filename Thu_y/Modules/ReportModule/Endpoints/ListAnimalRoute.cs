using Microsoft.AspNetCore.Authorization;
using Thu_y.Modules.ReportModule.Model;
using Thu_y.Modules.ReportModule.Ports;
using Thu_y.Utils.Infrastructure.Application.Models;

namespace Thu_y.Modules.ReportModule.Endpoints
{
    public class ListAnimalEndpoint
    {
        public const string Prefix = "";
        public const string BasePath = Prefix + "/listanimal";
        public const string GetListAni = BasePath;
        public const string CreateLsAnimal = BasePath + "/create";
        public const string UpdateLsAnimal = BasePath + "/update";
        public const string DeleteLsAnimal = BasePath + "/delete";
    }

    public static class ListAnimalRoute
    {
        public static IEndpointRouteBuilder MapListAnimalEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet(ListAnimalEndpoint.GetListAni, [Authorize(AuthenticationSchemes = "Bearer")] (string reportId, IListAnimalService listAnimalService) =>
            {
                try
                {
                    var animal = listAnimalService.GetListAnimalByReportId(reportId);

                    return Results.Ok(value: new ResponseModel<ICollection<ListAnimalModel>>(data: animal));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.Json(new ResponseModel<ICollection<ListAnimalModel>>(message: ex.Message), statusCode: ex.HResult);
                    }
                    return Results.Json(new ResponseModel<ICollection<ListAnimalModel>>(message: ex.Message), statusCode: 500);
                }
            }).WithTags(ListAnimalEndpoint.BasePath);

            endpoints.MapPost(ListAnimalEndpoint.CreateLsAnimal, [Authorize(AuthenticationSchemes = "Bearer")] async (ListAnimalModel model, IListAnimalService listAnimalService) =>
            {
                try
                {
                    var data = listAnimalService.CreateAsync(model);
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
            }).WithTags(ListAnimalEndpoint.BasePath);

            endpoints.MapPost(ListAnimalEndpoint.UpdateLsAnimal, [Authorize(AuthenticationSchemes = "Bearer")] async (UpdateListAnimalModel model, IListAnimalService listAnimalService) =>
            {
                try
                {
                    await listAnimalService.UpdateAsync(model);
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
            }).WithTags(ListAnimalEndpoint.BasePath);

            endpoints.MapPost(ListAnimalEndpoint.DeleteLsAnimal, [Authorize(AuthenticationSchemes = "Bearer")] async (DeleteModel request, IListAnimalService listAnimalService) =>
            {
                try
                {
                    await listAnimalService.DeleteAsync(request.Id);
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

            }).WithTags(ListAnimalEndpoint.BasePath);

            return endpoints;
        }
    }
}
