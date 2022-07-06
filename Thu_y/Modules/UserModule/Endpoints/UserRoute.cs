using Thu_y.Modules.UserModule.Ports;
using Thu_y.Modules.UserModule.Model;
using Thu_y.Utils.Infrastructure.Application.Models;

namespace Thu_y.Modules.UserModule.Endpoints
{
    public class UserEndpoint
    {
        public const string Prefix = "";
        public const string BasePath = Prefix + "/user";
        public const string GetUserById = BasePath;
        public const string CreateUser = BasePath + "/create-user";
        public const string UpdateUser = BasePath + "/update-user";
        public const string DeleteUser = BasePath + "/delete-user";
    }

    public static class UserRoute
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost(UserEndpoint.CreateUser, async (UserModel model, IUserService userService) =>
            {
                try
                {
                    if (userService.CreateUser(model))
                    {
                        return Results.Ok(value: new ResponseModel<string>(data: "Success"));
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
            }).WithTags(UserEndpoint.BasePath);

            endpoints.MapPut(UserEndpoint.UpdateUser, async (UserModel model, IUserService userService) =>
            {
                try
                {
                    if (userService.UpdateUser(model))
                    {
                        return Results.Ok(value: new ResponseModel<string>(data: "Success"));
                    }
                    throw new Exception("Failed to update!");
                }
                catch (Exception ex)
                {

                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.NotFound(new ResponseModel<string>(message: ex.Message));
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }
            }).WithTags(UserEndpoint.BasePath);

            endpoints.MapDelete(UserEndpoint.DeleteUser, async (string id, IUserService userService) =>
            {
                try
                {
                    if (userService.DeleteUser(id))
                    {
                        return Results.Ok(value: new ResponseModel<string>(data: "Success"));
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
            }).WithTags(UserEndpoint.BasePath);

            return endpoints;
        }
    }
}
