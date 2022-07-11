using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Thu_y.Modules.AbttoirModule.Model;
using Thu_y.Modules.AbttoirModule.Ports;
using Thu_y.Utils.Infrastructure.Application.Models;

namespace Thu_y.Modules.AbttoirModule.Endpoints
{
    public class AbattoirEndpoint
    {
        public const string Prefix = "";
        public const string BasePath = Prefix + "/abattoir";
        public const string GetAllAbattoir = BasePath;
        public const string CreateAbattoir = BasePath + "/create-abattoir";
        public const string UpdateAbattoir = BasePath + "/update-abattoir";
        public const string DeleteAbattoir = BasePath + "/delete-abattoir";
    }

    public static class AbattoirRoute
    {
        public static IEndpointRouteBuilder MapAbttoirEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost(AbattoirEndpoint.GetAllAbattoir, [Authorize(AuthenticationSchemes = "Bearer")] (AbttoirPagingModel model, IAbattoirRepository abattoirRepository, IMapper mapper) =>
            {
                try
                {
                    var receipts = abattoirRepository.Get(x =>
                    model.Id == null ? true : x.Id == model.Id &&
                    model.Name == null ? true : x.Name == model.Name &&
                    model.Address == null ? true : x.Address == model.Address &&
                    model.ManagerName == null ? true : x.ManagerName == model.ManagerName &&
                    model.Email == null ? true : x.Email == model.Email &&
                    model.Phone == null ? true : x.Phone == model.Phone)
                    .Skip(model.PageNumber * model.PageSize)
                    .Take(model.PageSize);

                    return Results.Ok(value: new ResponseModel<List<AbattoirModel>>(mapper.ProjectTo<AbattoirModel>(receipts).ToList()));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: ex.HResult);
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }
            }).WithTags(AbattoirEndpoint.BasePath);

            endpoints.MapPost(AbattoirEndpoint.CreateAbattoir, [Authorize(AuthenticationSchemes = "Bearer")] async (AbattoirModel model, IAbattoirService abattoirService) =>
            {
                try
                {
                    await abattoirService.CreateAsync(model);
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
            }).WithTags(AbattoirEndpoint.BasePath);

            endpoints.MapPost(AbattoirEndpoint.UpdateAbattoir, [Authorize(AuthenticationSchemes = "Bearer")] async (AbattoirModel model, IAbattoirService abattoirService) =>
            {
                try
                {
                    await abattoirService.UpdateAsync(model);
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
            }).WithTags(AbattoirEndpoint.BasePath);

            endpoints.MapDelete(AbattoirEndpoint.DeleteAbattoir, [Authorize(AuthenticationSchemes = "Bearer")] async (string id, IAbattoirService abattoirService) =>
            {
                try
                {
                    await abattoirService.DeleteAsync(id);
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

            }).WithTags(AbattoirEndpoint.BasePath);

            return endpoints;
        }
    }
}
