using Microsoft.EntityFrameworkCore;
using Thu_y.Modules.UserModule.Core;
using Thu_y.Modules.UserModule.Model;
using Thu_y.Modules.UserModule.Ports;
using Thu_y.Utils.Infrastructure.Application.Models;

namespace Thu_y.Modules.UserModule.Endpoints
{
    public class ScheduleEndpoint
    {
        public const string Prefix = "";
        public const string BasePath = Prefix + "/schedule";
        public const string ScheduleById = BasePath + "/get-user-schedule";
        public const string CreateSchedule = BasePath + "/create-schedule";
        public const string UpdateSchedule = BasePath + "/update-schedule";
        public const string DeleteSchedule = BasePath + "/delete-schedule";
    }

    public static class ScheduleRoute
    {
        public static IEndpointRouteBuilder MapScheduleEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet(ScheduleEndpoint.ScheduleById, async (string id, IUserRepository userRepository) =>
            {
                try
                {
                    var user = userRepository.Get(x => x.Id.Equals(id)).Include(x=>x.UserSchedules).FirstOrDefault();
                    if (user == null) throw new Exception("User not found!") { HResult = 404 };
                    return Results.Ok(new ResponseModel<UserEntity>(user));
                }
                catch (Exception ex)
                {

                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.NotFound(new ResponseModel<string>(message: ex.Message));
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }
            }).WithTags(ScheduleEndpoint.BasePath);

            endpoints.MapPost(ScheduleEndpoint.CreateSchedule, async (ScheduleModel model, IScheduleService scheduleService) =>
            {
                try
                {
                    if (scheduleService.CreateSchedule(model))
                    {
                        return Results.Ok(value: new ResponseModel<string>("Success"));
                    }
                    throw new Exception("Failed to create!");
                }
                catch (Exception ex)
                {

                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.NotFound(new ResponseModel<string>(message: ex.Message));
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }
            }).WithTags(ScheduleEndpoint.BasePath);

            endpoints.MapPut(ScheduleEndpoint.UpdateSchedule, async (UpdateScheduleModel model, IScheduleService scheduleService) =>
            {
                try
                {
                    if (scheduleService.UpdateSchedule(model))
                    {
                        return Results.Ok(value: new ResponseModel<string>("Success"));
                    }
                    throw new Exception("Failed to create!");
                }
                catch (Exception ex)
                {

                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.NotFound(new ResponseModel<string>(message: ex.Message));
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }
            }).WithTags(ScheduleEndpoint.BasePath);

            endpoints.MapDelete(ScheduleEndpoint.DeleteSchedule, async (string id, IScheduleService scheduleService) =>
            {
                try
                {
                    if (scheduleService.DeleteSchedule(id))
                    {
                        return Results.Ok(value: new ResponseModel<string>("Success"));
                    }
                    throw new Exception("Failed to delete!");
                }
                catch (Exception ex)
                {

                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.NotFound(new ResponseModel<string>(message: ex.Message));
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }
            }).WithTags(ScheduleEndpoint.BasePath);

            return endpoints;
        }
    }
}
