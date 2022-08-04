using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Thu_y.Infrastructure.Utils.Constant;
using Thu_y.Infrastructure.Utils.Exceptions;
using Thu_y.Modules.UserModule.Core;
using Thu_y.Modules.UserModule.Model;
using Thu_y.Modules.UserModule.Ports;
using Thu_y.Utils.Infrastructure.Application.Models;

namespace Thu_y.Modules.UserModule.Endpoints
{
    public class UserEndpoint
    {
        public const string Prefix = "";
        public const string BasePath = Prefix + "/user";
        public const string CreateUser = BasePath + "/create-user";
        public const string UpdateUser = BasePath + "/update-user";
        public const string DeleteUser = BasePath + "/delete-user";
        public const string GetUser = BasePath + "/get-user";
        public const string GetSingeUser = BasePath + "/get-single";
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

            endpoints.MapPost(UserEndpoint.UpdateUser, async (UserModel model, IUserService userService) =>
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

            endpoints.MapPost(UserEndpoint.DeleteUser, async (string id, IUserService userService, IHttpContextAccessor httpContext) =>
            {
                try
                {
                    var userLogger = (UserEntity)httpContext.HttpContext.Items["UserEntity"];
                    if (userLogger == null) return Results.Unauthorized();
                    if (userLogger.Role != RoleType.Manager) return Results.Unauthorized();

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

            endpoints.MapGet(UserEndpoint.GetUser, [Authorize(AuthenticationSchemes = "Bearer")]  (int pageIndex, int pageNumber,IUserService userService, IMapper mapper) =>
            {
                try
                {
                    var listUser = userService.GetAccount(pageIndex,pageNumber);
                    if (listUser == null) throw new Exception("NoT found form!") { HResult = 400 };

                    return Results.Ok(value: new ResponseModel<ICollection<UserGetListModel>>(mapper.Map<ICollection<UserGetListModel>>(listUser)));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: ex.HResult);
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }
            }).WithTags(UserEndpoint.BasePath);

            endpoints.MapGet(UserEndpoint.GetSingeUser, [Authorize(AuthenticationSchemes = "Bearer")]  ( string id,IUserService userService) =>
            {
                try
                {
                    var user = userService.GetUserById(id);
                    if (user == null) throw new AppException($"Not found user with id {id}!");

                    return Results.Ok(value: new ResponseModel<UserGetListModel>(data: user));
                }
                catch (Exception ex)
                {
                    if (ex.HResult >= 400 && ex.HResult < 500)
                    {
                        return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: ex.HResult);
                    }
                    return Results.Json(new ResponseModel<string>(message: ex.Message), statusCode: 500);
                }
            }).WithTags(UserEndpoint.BasePath);

            return endpoints;
        }
    }
}
