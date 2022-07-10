using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Thu_y.Modules.ShareModule.Model;
using Thu_y.Modules.ShareModule.Ports;
using Thu_y.Utils.Infrastructure.Application.Models;

namespace Thu_y.Modules.ShareModule.Endpoints
{
    public class AnimalEndpoint
    {
        public const string Prefix = "";
        public const string BasePath = Prefix + "/animal";
        public const string GetAllAnimal = BasePath;
        public const string CreateAnimal = BasePath + "/create-animal";
        public const string UpdateAnimal = BasePath + "/update-animal";
        public const string DeleteAnimal = BasePath + "/delete-animal";
    }
    public static class AnimalRoute
    {
        public static IEndpointRouteBuilder MapAnimalEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost(AnimalEndpoint.GetAllAnimal, [Authorize(AuthenticationSchemes = "Bearer")] (AnimalPagingModel model, IAnimalRepository animalRepository, IMapper mapper) =>
            {
                try
                {
                    var receipts = animalRepository.Get(x =>
                    model.Id == null ? true : x.Id == model.Id &&
                    model.Name == null ? true : x.Name == model.Name &&
                    model.DayAge == null ? true : x.DayAge == model.DayAge &&
                    model.Sex == null ? true : x.Sex == model.Sex)
                    .Skip(model.PageNumber * model.PageSize)
                    .Take(model.PageSize);

                    return Results.Ok(value: new ResponseModel<List<AnimalModel>>(mapper.ProjectTo<AnimalModel>(receipts).ToList()));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: ex.HResult);
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }
            }).WithTags(AnimalEndpoint.BasePath);

            endpoints.MapPost(AnimalEndpoint.CreateAnimal, [Authorize(AuthenticationSchemes = "Bearer")] async (AnimalModel model, IAnimalService animalService) =>
            {
                try
                {
                    await animalService.CreateAsync(model);
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
            }).WithTags(AnimalEndpoint.BasePath);

            endpoints.MapPost(AnimalEndpoint.UpdateAnimal, [Authorize(AuthenticationSchemes = "Bearer")] async (AnimalModel model, IAnimalService animalService) =>
            {
                try
                {
                    await animalService.UpdateAsync(model);
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
            }).WithTags(AnimalEndpoint.BasePath);

            endpoints.MapDelete(AnimalEndpoint.DeleteAnimal, [Authorize(AuthenticationSchemes = "Bearer")] async (string id, IAnimalService animalService) =>
            {
                try
                {
                    await animalService.DeleteAsync(id);
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

            }).WithTags(AnimalEndpoint.BasePath);

            return endpoints;
        }
    }
}
